$(function () {
    $('.multiselect-add, .multiselect-remove, .multiselect-move-up, .multiselect-move-down').prop("disabled", true);

    $('.multiselect-add, .multiselect-remove, .multiselect-addall, .multiselect-removeall').on("click",
        function () {
            ArrowBtns_OnClick(this);
        }
    )
    $('.multiselect-from').on("click",
        function () {
            DualList_OnChange(this);
        }
    );
    $('.multiselect-to').on("click",
        function () {
            DualList_OnChange(this);
        }
    );
    $(".reset-filter").on("click", function () {
        $(this).parent().parent().parent().find(".multiselect-removeall").trigger("click");
    });
    $('.multiselect-dual').multiselectDual({
        sort: false,
        ignoreDisabled: true,
        afterInit: function () {
        },
        afterMoveToLeft: function () {
            listBoxLabel();
        },
        afterMoveToRight: function () {
            listBoxLabel();
        },
        beforeMoveUp: function ($options) {
            return beforeMoveUp($options);
        }
    });

    $(".multiselect-from").on("change", function () {
        $(".multiselect-to").val([]);
    });

    $(".multiselect-to").on("change", function () {
        $(".multiselect-from").val([]);
    });

    listBoxLabel();

    function beforeMoveUp(options) {
        var disabledAttr = options.prev().attr('disabled');

        if (typeof disabledAttr !== 'undefined' && disabledAttr === "disabled") {
            return false;
        }
        else {
            return true;
        }
    }
});

function ArrowBtns_OnClick(btn) {
    var container = $(btn).parent();
    ArrowsBtnControl(container, $(btn));
}

function DualList_OnChange(list) {
    var container = $(list).parent().parent().find(".button-container");
    ArrowsBtnControl(container, $(list));
}

function ArrowsBtnControl(container, element) {

    var btns_arrow = [];
    var add, remove, addall, removeall;

    setTimeout(
        function () {
            $(container).find("button").each(
                function (index) {
                    btns_arrow[index] = $(this).attr("id");
                });
            add = btns_arrow[0];
            remove = btns_arrow[1];
            addall = btns_arrow[2];
            removeall = btns_arrow[3];
            if ($(element).prop("tagName") == "BUTTON") {

                var isFromListEmpty = $(container).parent().parent().find('.multiselect-from').children('option').length == 0;
                var isToListEmpty = $(container).parent().parent().find('.multiselect-to').children('option').length == 0;

                $('#' + remove).prop("disabled", true);
                $('#' + add).prop("disabled", true);
                $('.multiselect-move-up, .multiselect-move-down').prop("disabled", true);

                $('#' + addall).prop("disabled", isFromListEmpty);
                $('#' + removeall).prop("disabled", isToListEmpty);
            }

            if ($(element).prop("tagName") == "SELECT") {
                var isFromList = $(element).hasClass('multiselect-from');
                $('#' + remove).prop("disabled", isFromList);
                $('#' + add).prop("disabled", !isFromList);
                if (!isFromList) {
                    moveButtonControl(element);
                }
            }

        }, 100);
}

function listBoxLabel() {
    $('.listbox').each(function () {
        var id = $(this).attr('id');
        var info = $(this).parent().find("span.info");
        var count = $(this).children('option').length;

        if (count == 0) {
            info.html("Empty list");
        }
        else {
            info.html("Showing all " + count);
        }
    });
}

function moveButtonControl(element) {
    var selected = $(element).find('option:selected');
    var isMoveUpDisabled = typeof selected.first().prev().val() == "undefined";
    var isMoveDownDisabled = typeof selected.next().val() == "undefined";
    $('.multiselect-move-up').prop("disabled", isMoveUpDisabled);
    $('.multiselect-move-down').prop("disabled", isMoveDownDisabled);

}