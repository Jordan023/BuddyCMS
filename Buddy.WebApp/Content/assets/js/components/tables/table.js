// Builds the HTML Table out of JSON.
function buildHtmlTable(json, selector) {
    var columns = addAllColumnHeaders(json, selector);

    var Tbody$ = $('<tbody/>');
    for (var i = 0; i < json.length; i++) {
        var row$ = $('<tr/>');
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
            var cellValue = json[i][columns[colIndex]];
            if (cellValue === null) cellValue = "";
            row$.append($('<td/>').html(cellValue));
        }
        Tbody$.append(row$);
        $(selector).append(Tbody$);
    }
}

// Adds a header row to the table and returns the set of columns.
// Need to do union of keys from all records as some records may not contain
// all records.
function addAllColumnHeaders(json, selector) {
    var columnSet = [];

    var headerThead$ = $('<thead/>');
    var headerTr$ = $('<tr/>');
    headerThead$.append(headerTr$);
    
    for (var i = 0; i < json.length; i++) {
        var rowHash = json[i];
        for (var key in rowHash) {
            if ($.inArray(key, columnSet) === -1) {
                columnSet.push(key);
                var headerTh$ = $('<th class="secondary-text"/>');
                var headerDiv$ = $('<div class="table-header"/>');
                var headerSpan$ = $('<span class="column-title"/>').html(key);
                headerDiv$.append(headerSpan$);
                headerTh$.append(headerDiv$);
                headerTr$.append(headerTh$);
            }
        }
    }
    $(selector).append(headerThead$);

    return columnSet;
}