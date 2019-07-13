function parseInformationAboutUser() {
    var browseResultsContainer = document.getElementById('BrowseResultsContainer');
    if (browseResultsContainer) {
        var items = browseResultsContainer.childNodes;
        if (items && items.length > 0) {
            var array = new Array();
            for (var i = 0; i < items.length; i++) {
                var tagA = items[i].querySelector('a');
                if (tagA) {
                    array.push({ 'firstAndLastName': tagA.title, 'uriToFacebook': tagA.href });
                }
            }
        }
        return array;
    }
    return null;
}
window.informationAboutUsers = parseInformationAboutUser();