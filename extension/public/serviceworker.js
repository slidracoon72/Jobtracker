
console.log('Service worker script started');

var bookmarked = []

// Listen for messages sent from the content script or other parts of the extension
chrome.runtime.onMessage.addListener(async (message, sender, sendResponse) => {
  // 2. A page requested user data, respond with a copy of `user`

  // Check if the received message is 'onclicked'
  if (message.message === 'onclicked') {

    // Query the active tab to get information about it
    var tab = await chrome.tabs.query({ "active": true });

    console.log(tab);
    // Log the pending URL of the active tab
    message.url = tab[0].pendingUrl;
    message["status"] = 0;

    message["id"] = "00000000-0000-0000-0000-000000000000";
    // console.log(JSON.parse(localStorage.getItem("user")));
    // console.log(localStorage.getItem("access_token"));
    message["userId"] = "00000000-0000-0000-0000-000000000000";
    // Log the updated message

    // Check if the message has the property "message" and remove it
    if (message.hasOwnProperty("message")) {
      delete message["message"];
    }

    // Add "CreatedDate" and "UpdatedDate" properties with current timestamps
    message["CreatedDate"] = new Date().toISOString();
    message["UpdatedDate"] = new Date().toISOString();

    console.log(message);


    chrome.storage.local.get('myData', function (result) {
      console.log("Here in local data");
      message.userId = JSON.parse(result.myData.user).id;
      fetch('https://jobtrackerapi-asc.azurewebsites.net/Job', {
        method: 'POST',
        headers: {
          Authorization: `Bearer ` + result.myData.access_token,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(message)
      })
        .then(response => response.json())
        .then(data => sendResponse(data))
        .catch(error => sendResponse(error));
    });


    // Send a POST request to the specified API endpoint with the modified message

    sendResponse("null");

  }
  if (message.message === "onclickedWeb") {
    console.log("Here on web click");
    var tab = await chrome.tabs.query({ "active": true });

    console.log(tab);
    console.log(chrome);
    // Log the pending URL of the active tab
    message.url = tab[0].pendingUrl;
    message["status"] = 0;

    message["id"] = "00000000-0000-0000-0000-000000000000";
    // console.log(JSON.parse(localStorage.getItem("user")));
    // console.log(localStorage.getItem("access_token"));
    message["userId"] = "00000000-0000-0000-0000-000000000000";
    // Log the updated message

    // Check if the message has the property "message" and remove it
    if (message.hasOwnProperty("message")) {
      delete message["message"];
    }

    // Add "CreatedDate" and "UpdatedDate" properties with current timestamps
    message["CreatedDate"] = new Date().toISOString();
    message["UpdatedDate"] = new Date().toISOString();

    if (chrome.bookmarks) {
      chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        var currentTab = tabs[0];

        // Query bookmarks to check if the current URL is already bookmarked
        chrome.bookmarks.search({ url: currentTab.pendingUrl }, function (results) {
          if (results.length > 0) {
            console.log('This page is already bookmarked!');
          } else {
            // Specify the folder name where you want to add the bookmark
            var folderName = 'JobTracker_Jobs'; // Replace with the actual folder name

            // Look up the folder ID by name and then bookmark the page
            getOrCreateFolder(folderName, function (folderId) {
              bookmarkCurrentPage(currentTab, folderId, message["title"]);
            });
          }
        });
      });
    } else {
      console.error('Chrome bookmarks API not available.');
    }

    chrome.storage.local.get('myData', function (result) {
      console.log("Here in local data");
      message.userId = JSON.parse(result.myData.user).id;
      console.log(message);
      fetch('https://jobtrackerapi-asc.azurewebsites.net/Job', {
        method: 'POST',
        headers: {
          Authorization: `Bearer ` + result.myData.access_token,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(message)
      })
        .then(response => response.json())
        .then(data => sendResponse(data))
        .catch(error => sendResponse(error));
    });


    // Send a POST request to the specified API endpoint with the modified message

    sendResponse("null");
  }
});

function getOrCreateFolder(folderName, callback) {
  // Check if the bookmarks API is available
  if (chrome.bookmarks) {
    // Retrieve the entire bookmarks tree
    chrome.bookmarks.getTree(function (bookmarksTree) {
      // Find the folder in the bookmarks tree
      var folderId = findFolderId(bookmarksTree, folderName);

     
      // If the folder is found, use its ID; otherwise, create the folder
      if (folderId) {
        callback(folderId);
      } else {
        chrome.bookmarks.create({ parentId: '1', title: folderName }, function (folder) {
          callback(folder.id);
        });
      }
    });
  } else {
    console.error('Chrome bookmarks API not available.');
  }
}

function findFolderId(bookmarks, folderName) {
  // Recursive function to find the folder ID by name
  for (let i = 0; i < bookmarks.length; i++) {
    const bookmark = bookmarks[i];
    if (bookmark.title === folderName) {
      console.log(bookmark);
      console.log(bookmark.id);
      return bookmark.id;
    }
    if (bookmark.children) {
      const folderId = findFolderId(bookmark.children, folderName);
      if (folderId) {
        return folderId;
      }
    }
  }
  return null; // Folder not found
}

function bookmarkCurrentPage(tab, folderId, title) {
  // Check if the bookmarks API is available
  if (chrome.bookmarks) {
    chrome.bookmarks.search({ title: title, url: tab.pendingUrl }, function (results) {
      if (results.length <= 0 && !bookmarked.includes(tab.pendingUrl)) {
        bookmarked.push(tab.pendingUrl);
        chrome.bookmarks.create({
          parentId: folderId,
          title: title,
          url: tab.pendingUrl
        }, function (result) {
          if (chrome.runtime.lastError) {
            console.error(chrome.runtime.lastError);
          } else {
            console.log('Bookmark created successfully!');
          }
        });
      } else {
        console.log('Chrome bookmarks API not available.');
      }
    });
  }
  else {
    console.log('Chrome bookmarks API not available.');
  }
}

chrome.runtime.onInstalled.addListener(() => {
  console.log(chrome);
});

