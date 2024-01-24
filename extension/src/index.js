import React from "react";
import ReactDOM from "react-dom/client";
import { initializeApp } from "firebase/app";
import {
  getAuth,
  signOut,
  signInWithCredential,
  GoogleAuthProvider,
} from "firebase/auth";
import { FIREBASE_CONFIG } from "./const";
import reportWebVitals from "./reportWebVitals";
import axios from 'axios';

export var firebase = initializeApp(FIREBASE_CONFIG);
export var auth = getAuth(firebase);

export const App = () => {
  const [user, setUser] = React.useState(undefined);

  const moredetails = () => {
    // Redirect to index.html in the template
    chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
      chrome.tabs.create({ url: chrome.runtime.getURL("/ui/index.html") });
    });
  };

  const [loading, setLoading] = React.useState(false);


  function transformJson(json) {
    const transformedModel = {
      Id: "00000000-0000-0000-0000-000000000000",
      Uid: json.user.uid,
      Email: json.user.email,
      EmailVerified: json.user.emailVerified,
      IsAnonymous: json.user.isAnonymous,
      PhotoURL: json.user.photoURL,
      ProviderData: json.user.providerData.map((provider) => ({
        Id: "00000000-0000-0000-0000-000000000000",
        ProviderId: provider.providerId,
        Uid: provider.uid,
        DisplayName: provider.displayName,
        Email: provider.email,
        PhoneNumber: provider.phoneNumber,
        PhotoURL: provider.photoURL,
        SingleSignOnId: "00000000-0000-0000-0000-000000000000",
      })),
      RefreshToken: json.user.stsTokenManager.refreshToken,
      AccessToken: json.user.stsTokenManager.accessToken,
      OauthExpireIn: json.user.stsTokenManager.oauthExpireIn,
      ExpiresIn: json.user.stsTokenManager.expiresIn,
      RawUserInfo: json.user.stsTokenManager.rawUserInfo,
      Kind: json.user.stsTokenManager.kind,
      OperationType: json.user.operationType,
      UserId: "00000000-0000-0000-0000-000000000000",
    };

    return transformedModel;
  }

  const signIn = (e) => {
    setLoading(true);


    // Check if the extension has incognito access

    chrome.identity.getAuthToken({ interactive: true, scopes: ['profile', 'email'] }, (token) => {
      if (chrome.runtime.lastError || !token) {
        alert(`SSO ended with an error: ${JSON.stringify(chrome.runtime.lastError)}`);
        return;
      }
      signInWithCredential(auth, GoogleAuthProvider.credential(null, token))
        .then(async (res) => {
          try {
            const accessToken = res.user.stsTokenManager.accessToken;
            const apiUrl = 'https://jobtrackerapi-asc.azurewebsites.net/Auth/';
            const headers = {
              Authorization: `Bearer ${accessToken}`,
              'Content-Type': 'application/json',
            };

            // Transform user data
            const transformedModel = transformJson(res);


            // Make API call to save user data on the server
            fetch(apiUrl, {
              method: 'POST',
              headers: headers,
              body: JSON.stringify(transformedModel)
            })
              .then(response => response.json())
              .then(data => {
                //alert(JSON.stringify(data));
                // Set the user in the component state

                const dataToStore = { user: JSON.stringify(data), "access_token": accessToken };
                setUser(data);
                setLoading(false);
                //alert(JSON.stringify(dataToStore));
                // Save data to chrome.storage.local
                chrome.storage.local.set({ myData: dataToStore }, function () {
                  console.log('Data saved in content script');
                });

                document.getElementById("clickButton").click();

                chrome.storage.local.get('myData', function (result) {
                  fetch(
                    "https://jobtrackerapi-asc.azurewebsites.net/User?userId=" + JSON.parse(result.myData.user).id, {
                    method: 'GET',
                    headers: {
                      Authorization: `Bearer ` + result.myData.access_token,
                      'Content-Type': 'application/json'
                    }
                  }
                  )
                    .then((response) => response.json())
                    .then((data) => {
                      // Process the API response and update the job tiles
                      updateJobTiles(data);
                    })
                    .catch((error) => console.error("Error fetching API:", error));

                });

                //alert(JSON.parse(localStorage.getItem("user")).id);
              })
              .catch(error => console.error(error));
          } catch (error) {
            //alert(JSON.stringify(error));
            console.error('Error adding data to the database:', error);
          }
          console.log("signed in!");
        })
        .catch((err) => {
          console.log(err);
          //alert(`SSO ended with an error: ${err}`);
        });
    });
  };

  const callSync = (e) => {
    document.getElementById("clickButton").click();
  };

  const logout = (e) => {
    signOut(auth)
      .then(() => {
        setUser(null);
        document.getElementById("clickButton").style.display = "none";
        chrome.storage.local.remove("myData", function () {
          console.log('myData removed from storage');
        });
      })
      .catch((err) => {
        console.error(`Logout error: ${err}`);
      });
  };

  React.useEffect(() => {
    chrome.storage.local.get("myData", function (result) {
      if (result.myData) {
        // The data exists in storage
        const userData = JSON.parse(result.myData.user);
        console.log('User data from storage:', userData);
        // Now you can set your React component state using setUser(userData)
        setUser(userData);
        setLoading(false);

        fetch(
          "https://jobtrackerapi-asc.azurewebsites.net/User?userId=" + JSON.parse(result.myData.user).id, {
          method: 'GET',
          headers: {
            Authorization: `Bearer ` + result.myData.access_token,
            'Content-Type': 'application/json'
          }
        }
        )
          .then((response) => response.json())
          .then((data) => {
            // Process the API response and update the job tiles
            updateJobTiles(data);
          })
          .catch((error) => {
            setUser(null);
            console.error("Error fetching API:", error)
          });

      } else {
        // The data doesn't exist in storage
        console.log('User data not found in storage');
        setUser(null);
      }
    });
  }, []);

  function updateJobTiles(apiResponse) {

    // Clear existing job tiles
    document.getElementById("joblist").innerHTML = "";
    var i = 0;
    // Iterate over the API response and fill job tiles
    apiResponse.forEach((job) => {
      if (i < 3) {
        // Create a new job item
        var jobItem = document.createElement("div");

        // Create the job item content
        var content = `
        <div class="row g-4">
        <div class="col-sm-12 col-md-8 d-flex align-items-center">
          <img class="flex-shrink-0 img-fluid border rounded" src="${job.logoUrl
          }" alt="" style="width: 80px; height: 80px;">
          <div class="text-start ps-4">
            <h5 class="mb-3">${job.title}</h5>
            <span class="text-truncate me-3"><i class="fa fa-map-marker-alt text-primary me-2"></i>${job.location
          }</span>
            
          </div>
        </div>
      </div>
      <br />
        `;

        // Set the content to the job item
        jobItem.innerHTML = content;

        jobItem.addEventListener("click", () => {
          // Redirect to the job details page with the job ID
          chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            chrome.tabs.create({ url: chrome.runtime.getURL("/ui/job-detail.html?jobId=" + job.id) });
          });
        });
        // Append the job item to the tab-1 container
        document.getElementById("joblist").appendChild(jobItem);
        i = i + 1;
      }
      else {
        return;
      }
    });
  }



  if (loading) return <img
    style={{ height: "100px", width: "100px" }}
    src="./icons/giphy.gif"
    alt="Loading..."
    className="loader-image"
  />;

  if (user != null) {
    return (
      <div style={{ width: "600px" }}>
        <div className="container-xxl bg-white p-0">


          <nav className="navbar navbar-expand-lg bg-white navbar-light shadow sticky-top p-0">
            <a href="#" className="navbar-brand d-flex align-items-center text-center py-0 px-4 px-lg-5">
              <h1 className="m-0 text-primary">JobTracker</h1>
            </a>
            <button type="button" className="navbar-toggler me-4" data-bs-toggle="collapse" data-bs-target="#navbarCollapse">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarCollapse">
              <div className="navbar-nav ms-auto p-4 p-lg-0">
                <a href="#" className="nav-item nav-link active">Signed in as {user.name}.</a>
                <a href="" onClick={callSync} className="nav-item nav-link">Sync In</a>
              </div>

              <a href="#" onClick={logout} className="nav-item nav-link">Sign Out</a>
            </div>

          </nav>
        </div>

        <div className="container-xxl py-5">
          <div className="container">
            <h1 className="text-center mb-5 wow fadeInUp" data-wow-delay="0.1s">Job Listing</h1>
            <div className="tab-class text-center wow fadeInUp" data-wow-delay="0.3s">
              <div id="joblist"></div>
            </div>
          </div>
          <a href="#" onClick={moredetails} className="nav-item nav-link text-center">Click here for More Details</a>
        </div>
      </div>
    );
  } else {

    // Render the "Sign In" button only if not in incognito mode
    return (
      <div>

        <button><img src="./icons/google-logo.png" alt="my image" onClick={signIn} /></button>
      </div>
    );
  }
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

reportWebVitals();
