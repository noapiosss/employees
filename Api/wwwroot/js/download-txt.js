function downloadStatistics(id)
{
    fetch(`/api/departments/statistics/${id}`)
        .then(response =>
        {
            if (!response.ok)
            {
            throw new Error('Network response was not ok ' + response.status);
            }
            
            const contentDisposition = response.headers.get('Content-Disposition');
            let filename = 'statistics.txt';
            
            if (contentDisposition && contentDisposition.indexOf('attachment') !== -1)
            {
                const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                const matches = filenameRegex.exec(contentDisposition);
                if (matches != null && matches[1])
                {
                    filename = matches[1].replace(/['"]/g, '');
                }
            }

            return downloadTxtFile(response.blob(), filename);
        })
        .catch(error =>
        {
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