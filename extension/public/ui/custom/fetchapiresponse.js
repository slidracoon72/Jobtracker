export function fetchAndDisplayData() {
  // Fetch the API data
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
}
export function getStatusLabel(status) {
  switch (status) {
    case 0:
      return "Applied";
    case 1:
      return "In Review";
    case 2:
      return "Incomplete";
    case 3:
      return "Rejected";
    case 4:
      return "PendingOA";
    case 5:
      return "Accepted";
    default:
      return "Unknown";
  }
}

function getdatestring(dateString) {
  const dateObject = new Date(dateString);

  const options = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
    second: 'numeric'
  };

  return new Intl.DateTimeFormat('en-US', options).format(dateObject);
}

function checkSalary(salary) {
  if (/\d/.test(salary)) {
    return salary;
  }
  return "Not Mentioned";
}

// Import useState and useEffect if not already imported

export function deleteJob(jobId) {
  console.log(jobId);
  //Make a DELETE request to the API endpoint with the job ID
  chrome.storage.local.get('myData', function (result) {
    fetch(`https://jobtrackerapi-asc.azurewebsites.net/Job/${jobId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ` + result.myData.access_token,
      },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`Failed to delete job with ID ${jobId}`);
        }
        // Handle success as needed, maybe refresh the job list
        console.log(`Job with ID ${jobId} deleted successfully`);
        fetchAndDisplayData();
        // You may want to update the job list after deletion
      })
      .catch(error => {
        console.error('Error deleting job:', error);
        // Handle errors as needed
      });
  });
}



export function updateJobTiles(apiResponse) {
  // Clear existing job tiles
  document.getElementById("tab-1").innerHTML = "";

  // Iterate over the API response and fill job tiles
  apiResponse.forEach((job) => {
    // Create a new job item
    var jobItem = document.createElement("div");
    jobItem.className = "job-item p-4 mb-4";
    jobItem.dataset.jobId = job.id; // Store the job ID as a data attribute

    // Create the job item content
    var content = `
      <div class="row g-4">
        <div class="col-sm-12 col-md-8 d-flex align-items-center">
          <img class="flex-shrink-0 img-fluid border rounded" src="${job.logoUrl}" alt="" style="width: 80px; height: 80px;">
          <div class="text-start ps-4">
            <h5 class="mb-3">${job.title}</h5>
            <span class="text-truncate me-3"><i class="fa fa-map-marker-alt text-primary me-2"></i>${job.location}</span>
            <span class="text-truncate me-0"><i class="far fa-money-bill-alt text-primary me-2"></i>${checkSalary(job.salary)}</span>
          </div>
        </div>
        <div class="col-sm-12 col-md-4 d-flex flex-column align-items-start align-items-md-end justify-content-center">
          <div class="d-flex mb-3">
            <a class="" href="">${getStatusLabel(job.status)}</a>
          </div>
          <small class="text-truncate"><i class="far fa-calendar-alt text-primary me-2"></i>Date: ${getdatestring(job.createdDate)}</small>
        </div>
      </div>
      
      <div class="row mt-3">
        <div class="col">
          <button class="btn btn-danger float-end delete-btn" style="width: 80px;"}">Delete</button>
        </div>
      </div>
    `;

    // Set the content to the job item
    jobItem.innerHTML = content;

    // Make the entire job tile clickable
    jobItem.addEventListener("click", (event) => {
      if (!event.target.classList.contains('delete-btn')) {
        window.location.href = `job-detail.html?jobId=${job.id}`;
      }
      else {
        deleteJob(job.id);
      }
    });

    // Append the job item to the tab-1 container
    document.getElementById("tab-1").appendChild(jobItem);
  });
}

// Function to convert an array of objects to CSV format
function convertObjectsToCSV(data) {
  // Exclude columns from header
  const header = Object.keys(data[0])
    .filter(key => !["userId", "id", "updatedDate", "jobKeywords", "logoUrl"].includes(key))
    .join(',') + '\n';

  // Exclude columns from rows
  const rows = data.map(obj => {
    return Object.values(obj)
      .filter((_, index) => !["userId", "id", "updatedDate", "jobKeywords", "logoUrl"].includes(Object.keys(obj)[index]))
      .map((value, index) => {

        ;
        // Handle multiline values by enclosing in double quotes
        if (typeof value === 'string' && (value.includes('\n') || value.includes(','))) {
          return `"${value.replace(/"/g, '""')}"`;
        }
        // Apply checkSalary function to "salary" column


        if (index === 5) {
          return getStatusLabel(parseInt(value, 10));
        }


        if (Object.keys(obj)[index] === "salary") {
          return checkSalary(value);
        }

        return value;
      })
      .join(',');
  }).join('\n');


  // Combine header and rows
  return header + rows;
}

// Function to trigger the download of the CSV file
function downloadCSV(csv, filename) {
  const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
  const link = document.createElement('a');
  const url = URL.createObjectURL(blob);
  link.href = url;
  link.setAttribute('download', filename);
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
}

document.addEventListener("DOMContentLoaded", function () {
  fetchAndDisplayData();

  document.getElementById('excel').addEventListener("click", () => {
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
          console.log(data);
          // Process the API response and update the job tiles
          const csvData = convertObjectsToCSV(data);

          // Trigger the download
          downloadCSV(csvData, 'exported_data.csv');
        })
        .catch((error) => console.error("Error fetching API:", error));

    });

  });
});


