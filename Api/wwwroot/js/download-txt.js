function downloadStatistics(id)
{
    fetch(`/api/departments/statistics/${id}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok ' + response.status);
        }
        return response.text();
      })
      .then(data => {
        downloadTxtFile(data, "statisctics.txt");
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
}

function downloadTxtFile(data, filename)
{
    const blob = new Blob([data], { type: 'text/plain' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    link.click();
}