document.addEventListener("DOMContentLoaded", function () {
    const addJobForm = document.getElementById("addJobForm");
  
    addJobForm.addEventListener("submit", function (event) {
      event.preventDefault();
  
      // Get form values
      const title = document.getElementById("title").value;
      const location = document.getElementById("location").value;
      const salary = document.getElementById("salary").value;
  
      // Create a JSON object representing the job
      const jobObject = {
        title: title,
        location: location,
        salary: salary,
        // Add more fields as needed
      };
  
      // Log the jobObject to the console (you can send it to the server or update UI)
      console.log("Job Object:", jobObject);
  
      // Clear the form
      addJobForm.reset();
    });
  });
  