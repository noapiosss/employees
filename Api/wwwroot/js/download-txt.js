function createAndDownloadTxt(content)
{
    let result = content.replaceAll("|", "\n");
    const link = document.createElement("a");
    const file = new Blob([result], { type: 'text/plain' });
    link.href = URL.createObjectURL(file);
    link.download = "department-statistics.txt";
    link.click();
    URL.revokeObjectURL(link.href);
}