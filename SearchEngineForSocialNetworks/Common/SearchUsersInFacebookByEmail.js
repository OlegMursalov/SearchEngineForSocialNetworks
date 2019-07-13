var nodeList = document.getElementsByName('q');
if (nodeList && nodeList.length > 0) {
    nodeList[0].value = "{{query}}";
    nodeList = document.querySelectorAll('form[method="get"]');
    if (nodeList && nodeList.length > 0) {
        nodeList[0].submit();
    }
}