$(function () {
   
    initialize();

    $('#dashboard-filter-modal').on('show.bs.modal', function (event) {
        loadfilterlist();
    })

    $('[data-toggle="tooltip"]').tooltip();

    $("td").tooltip({ container: 'body' });

    $(".hidsel").hide();

    $(".save-filter").on("click", function () {
        savebuttonclick();
        loadprojecttable();
    });

    //$(".reset-filter").on("click", function () {
    //    $('#multiselect_leftAll').click();
    //    //var resetList = $("#ResetList").val();
    //    //resetlists(resetList);
    //});

    $(".cancel-filter").on("click", function () {
        $("#dashboard-filter-modal").modal("hide");
    });

    function initialize() {

        var resetList = $("#ResetList").val();

        var savedList = $("#SavedFilterList").val();

        var sortableListObject = $("#SortableList").val();

        if (savedList == "") {
            savedList = resetList;
        }

        var arrSavedList = splitsavedlist(savedList);

        var sortableListObject = JSON.parse(sortableListObject);

        sortableList = buildsortablelist(arrSavedList, sortableListObject);

        setupfilterlist(sortableList);

        setupsavefilterlist(sortableList, arrSavedList);

        loadprojecttable();
    }

    function resetlists(savedList) {

        $(".multiselect").empty();

        setupfilterlist(sortableList);

        setupsavefilterlist(sortableList, savedList);

        loadprojecttable();
    }

    function splitsavedlist(savedlist) {
        if (savedlist != null && savedlist != "")
            return savedlist.split(",");
        return [];
    }

    //make sortablelist an object
    function buildsortablelist(savedlist, sortablelist) {

        $.each(sortablelist, function (i, item) {
            item.selected = selectsavedlist(item.id, savedlist);
            item.list = item.selected == 0 ? 1 : 0;
            item.order = savedlist.indexOf(item.id);
            item.required = item.required
        });

        return sortablelist;
    }

    //save the saved list
    function savebuttonclick() {
        var dashboardType = $("#DashboardType").val();

        //save csv of th to input SavedFilterList
        var savedfilterlist = "";
        $("#multiselect_to option").each(function (index, element) {
            savedfilterlist += element.value + ",";
        });

        $("#SavedFilterList").val(savedfilterlist);
        //save to db
        var o = new Object();
        o.SaveFilterList = savedfilterlist;
        o.DashboardType = dashboardType;
        if (o != null) {
            $(".filter-info").html("Saving filter...");
            $.ajax({
                type: "POST",
                url: "/Estimation/SaveUserUIFilterOptions",
                data: JSON.stringify(o),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $(".filter-info").html("Save Success").show();
                },
                failure: function (response) { $(".filter-info").html("Save Error").show(); },
                error: function (response) { $(".filter-info").html("Save Error").show(); }
            });
        }
    }

    function selectsavedlist(key, savedlist) {
        var isselected = 0;
        $.each(savedlist, function (index, value) {
            if (value == key) {
                isselected = 1;
                return isselected;
            }
        });
        return isselected;
    }

    function setupfilterlist(sortablelist) {
        $("#multiselect").empty();

        var count = 0;

        for (var i = 0; i < sortablelist.length; i++) {
            if (sortablelist[i].list == "1" && sortablelist[i].required != "1") {
                count++;
                addtoselectlist("multiselect", "", sortablelist[i].id, sortablelist[i].header);
            }
        }

        updatelistcounts();
    }

    function setupsavefilterlist(sortablelist, arrsavedlist) {
        var count = 0;

        for (var i = 0; i < arrsavedlist.length; i++) {
            //find this id and send to select list
            var savedColumn = sortablelist.find(x => x.id == arrsavedlist[i]);
            if (savedColumn != null && savedColumn.selected == "1") {
                count++;
                addtoselectlist("multiselect_to", "", savedColumn.id, savedColumn.header);
            }
        }

        //update required items

         for (var i = 0; i < sortablelist.length; i++) {
            //find this id and send to select list

            var sortableItem = sortablelist[i];

             if (sortableItem != null && sortableItem.required == "1") {
                 $("#multiselect_to option[value='" + sortableItem.id + "']")
                        .attr("disabled", "disabled");
             }
        }

        updatelistcounts();
    }

    function updatelistcounts() {
        $('.multiselect-dual').each(function () {
            var id = $(this).attr('id');
            var info = $("#" + id).parent().find("span.info");
            var count = $('#' + id + ' option').length;
            if (count == 0) {
                info.html("Empty list");
            }
            else {
                info.html("Showing all " + count);
            }
        });
    }

    function loadprojecttable() {
        var savedfilterlist = [];
        var allcolslist = [];
        var hidelist = [];

        var dashboardType = $("#DashboardType").val();

        //if this is the estmation dashboard, make the first two columns fixed to the left
        if (dashboardType == "estimation" || dashboardType == "scheduling") {
            leftfixedcolumns = 2;
            allcolslist.push('0');
            allcolslist.push('1');
        }

        if (dashboardType == "meetings") {
            leftfixedcolumns = 1;
            allcolslist.push('0');
        }

        //get all the columns that are visible into sort order
        $("#multiselect_to option").each(function (index, element) {
            var colindex = element.value.replace("th", "");
            allcolslist.push(colindex);
            savedfilterlist.push(colindex);
            if (element.value == "th6" && dashboardType == "estimation") {
                //if this is estimation, have to show BEMP
                allcolslist.push('7'); allcolslist.push('8'); allcolslist.push('9');
            }
        });

        //add the columns in the filterlist to the sort order list
        $("#multiselect option").each(function (index, element) {
            var colindex = element.value.replace("th", "");
            allcolslist.push(colindex);
            hidelist.push(colindex);
            if (element.value == "th6" && dashboardType == "estimation") {
                //if this is estimation, have to hide BEMP
                allcolslist.push('7'); allcolslist.push('8'); allcolslist.push('9');
                hidelist.push('7'); hidelist.push('8'); hidelist.push('9');
            }
        });

        //find out if table has been initialised already
        var table;

        if ($.fn.dataTable.isDataTable('#projects')) {
            table = $('#projects').DataTable();
            //reset to original indexes, then reorder
            table.colReorder.reset();
            table.colReorder.order(allcolslist);
        }
        else {
             table = $('#projects').DataTable({
                height: getHeight(),
                colReorder: {
                    order: allcolslist,
                    fixedColumnsLeft: leftfixedcolumns                },
                //make horizontal scrollbar visible
                scrollX: true,
                fixedHeader: true
            });
        };

        //show columns from the saved list
        $.each(savedfilterlist, function (_, val) {
            // Get the column API object
            var column = table.column('.th' + val);

            // Toggle the visibility
            column.visible(true);

        });

        //hide columns from the filter list
        $.each(hidelist, function (_, val) {
            // Get the column API object
            var column = table.column('.th' + val);

            // Toggle the visibility
            column.visible(false);
        });
    }

    //when opening the filter list
    // make sure the saved list reflects the order in the table
    //   since users can pick columns up and move them around
    function loadfilterlist() {
        //get the table object
        var table = $('#projects').DataTable();

        var cols = [];
        //get the column class (ex. th0, th23) for the list
        table.columns().every(function () {
            var col = this;
            cols.push(col.header().className.split(" ")[0]);
        });

        //empty the saved list
        $("#multiselect_to").empty();

        //remove items in filter list (hidden/removed) from cols arra
        $("#multiselect option").each(function (_, element) {
            cols = cols.filter(e => e !== element.value)
        });

        //add the cols back in the correct order
        setupsavefilterlist(sortableList, cols);
    }

    function getHeight() {
        return $(window).height() - $('h1').outerHeight(true);
    }
});