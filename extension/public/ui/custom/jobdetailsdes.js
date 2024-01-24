// job-details.js
//import { getStatusLabel } from "./fetchapiresponse.js";
document.addEventListener("DOMContentLoaded", function () {
  // Get the job ID from the query parameters
  const urlParams = new URLSearchParams(window.location.search);
  const jobId = urlParams.get("jobId");

  // Fetch job details based on the jobId
  fetchJobDetails(jobId);

  document
    .getElementById("updateJobButton")
    .addEventListener("click", function () {
      document.getElementById('updateJobButton').style.display = "none";
      // Show the Submit button
      updateJobDetails();
      document.getElementById("submitButtonContainer").style.display = "block";
    });

  // Handle the submit logic
  document
    .getElementById("submitButton")
    .addEventListener("click", function () {
      // Call the submitForm function or add your submit logic here
      submitForm(jobId);
      document.getElementById("submitButtonContainer").style.display = "none";
      document.getElementById('updateJobButton').style.display = "block";
    });
});
function getStatusLabel(status) {
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
async function fetchJobDetails(jobId) {
  chrome.storage.local.get('myData', async function (result) {
    try {
      await fetch(
        "https://jobtrackerapi-asc.azurewebsites.net/Job/" + jobId, {
        method: 'GET',
        headers: {
          Authorization: `Bearer ` + result.myData.access_token,
          'Content-Type': 'application/json'
        }
      }
      ).then(async (response) => {
        displayJobDetails(await response.json());
      });
    } catch (error) {
      console.error("Error fetching job details:", error);
    }
  });
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

function displayJobDetails(jobDetails) {
  const jobDetailsContainer = document.getElementById("job-details-container");

  var content = `
      <div class="container-xxl py-5 wow fadeInUp" data-wow-delay="0.1s">
        <div class="container">
          <div class="row gy-5 gx-4">
            <div class="col-lg-8">
              <div class="d-flex align-items-center mb-5">
                <img class="flex-shrink-0 img-fluid border rounded" src="${jobDetails.logoUrl
    }" alt="Company Logo" style="width: 80px; height: 80px;">
                <div class="text-start ps-4">
                  <h3 class="mb-3">${jobDetails.title}</h3>
                  <span class="text-truncate me-3"><i class="fa fa-map-marker-alt text-primary me-2"></i>${jobDetails.location
    }</span>
                  
                  <span class="text-truncate me-3"><i class="far fa-money-bill-alt text-primary me-2"></i>${checkSalary(jobDetails.salary)
    }</span>

                  <span class="text-truncate me-3"><i class="far fa-calendar-alt text-primary me-2"></i>${getdatestring(jobDetails.createdDate)
    }</span>
                
                  <!-- <span class="text-truncate me-3"><i class="far fa-money-bill-alt text-primary me-2"></i>${getStatusLabel(
      jobDetails.status
    )}</span> -->
                  
               `;

  // Replace the static status display with a dynamic <select> element
  content += `

                                <select class="form-select" style="width:auto;" id="jobStatus" ${jobDetails.isEditable ? "" : "disabled"}>
  <option value="0" ${jobDetails.status === 0 ? "selected" : ""}>Applied</option>
  <option value="1" ${jobDetails.status === 1 ? "selected" : ""}>Reviewed</option>
  <option value="2" ${jobDetails.status === 2 ? "selected" : ""}>Incomplete</option>
  <option value="3" ${jobDetails.status === 3 ? "selected" : ""}>Rejected</option>
  <option value="4" ${jobDetails.status === 4 ? "selected" : ""}>Pending OA</option>
  <option value="5" ${jobDetails.status === 5 ? "selected" : ""}>Accepted</option>
</select>

                           
              
                  

                </div>
              </div>
  
              <div class="mb-5">
                <h4 class="mb-3">Job Description</h4>
                <iframe srcdoc="${jobDetails.jobKeywords
    }" width="100% height="100%" ></iframe>
              </div>
            </div>
          </div>
        </div>
      </div>
    `;

  // Update the content in the job details container
  jobDetailsContainer.innerHTML = content;
}

async function updateJobDetails() {
  var inputElements = document.querySelectorAll("input, select");

  // Toggle the 'disabled' attribute for each input and select
  inputElements.forEach(function (element) {
    element.disabled = !element.disabled;
  });
  //   const shouldEnableStatus = true; // Set this based on your condition

  //   // Get the status select element
  //   const statusSelect = document.getElementById("jobStatus");

  //   // Enable or disable the status button based on the condition
  //   statusSelect.disabled = !shouldEnableStatus;
}

function submitForm(jobId) {
  // Get all input and select elements by class name
  var inputElements = document.querySelectorAll("input, select");
  console.log(inputElements);
  chrome.storage.local.get('myData', async function (result) {
    // Make the PUT request using fetch
    fetch(`https://jobtrackerapi-asc.azurewebsites.net/Job/${jobId}?status=${parseInt(document.getElementById('jobStatus').value, 10)}`, {
      method: 'PUT',
      headers: {
        Authorization: `Bearer ` + result.myData.access_token,
        'Content-Type': 'application/json',
      }
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then(updatedJob => {
        // Handle the response data (updated job) here
        console.log('Job updated successfully:', updatedJob);
        fetchJobDetails(jobId);
      })
      .catch(error => {
        // Handle errors here
        console.error('Error updating job:', error);
      });
    // Disable all input and select elements
    inputElements.forEach(function (element) {
      element.disabled = true;
    });
  });
}
