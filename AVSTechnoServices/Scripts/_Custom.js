function Allowdigits(event, element) {
    if (isNaN(event.key) && !isAllowedKey(event)) {
        event.preventDefault();
    }
}
function isAllowedKey(event) {
    var allowed = false;
    if (event.keyCode === 8 || event.keyCode === 9 || event.keyCode === 37 || event.keyCode === 39) {
        allowed = true;
    }
    return allowed;
}

function limit(event, element, max_chars) {
    if (isTextSelected(element)) {																		//
        max_chars += 1;
    }
    if (element.value.length >= max_chars && !isAllowedKey(event)) {
        event.preventDefault();
    }
}

function ConverttoAmount(amount) {
    var number = Number(amount.replace(/[^0-9\.-]+/g, ""));
    return number;
}
function isTextSelected(input) {
    var startPosition = input.selectionStart;
    var endPosition = input.selectionEnd;

    var selObj = document.getSelection();
    var selectedText = selObj.toString();

    if (selectedText.length != 0) {
        input.focus();
        input.setSelectionRange(startPosition, endPosition);
        return true;
    } else if (input.value.substring(startPosition, endPosition).length != 0) {
        input.focus();
        input.setSelectionRange(startPosition, endPosition);
        return true;
    }
    return false;
}

function convertToAmountFormate(amount) {
    const formatter = new Intl.NumberFormat('en-IN', {
        style: 'currency',
        currency: 'INR',
        minimumFractionDigits: 2
    });
    if (isNaN(amount))
        amount = 0;
    var currency = formatter.format(amount).replace("₹", "");
    //var currency = formatter.format(amount);
    return currency;
}

//Without Index based script
function AmountformatIdx(inputName) {
    const formatter = new Intl.NumberFormat('en-IN', {
        style: 'currency',
        currency: 'INR',
        minimumFractionDigits: 2
    });
    var amount = parseInt($('#' + inputName).val());
    if (isNaN(amount))
        amount = 0;

    var currency = formatter.format(amount).replace("₹", "");
    $('#' + inputName).val(currency);
}

function AmountfocusIdx(inputName) {
    var number = converAmountIdx(inputName);
    $('#' + inputName).val(number);
}

function converAmountIdx(inputName) {
    var currency = $('#' + inputName).val();
    var number = Number(currency.replace(/[^0-9\.-]+/g, ""));
    return number;
}



//Index based scripts
function Amountformat(Idx, inputName) {
    const formatter = new Intl.NumberFormat('en-IN', {
        style: 'currency',
        currency: 'INR',
        minimumFractionDigits: 2
    });
    var amount = parseInt($('#' + inputName + Idx).val());
    if (isNaN(amount))
        amount = 0;

    //var currency = formatter.format(amount).replace("$", "");
    var currency = formatter.format(amount).replace("$", "");
    $('#' + inputName + Idx).val(currency);
}

function Amountfocus(Idx, inputName) {
    var number = converAmount(Idx, inputName);
    $('#' + inputName + Idx).val(number);
}

function converAmount(Idx, inputName) {
    var currency = $('#' + inputName + Idx).val();
    var number = Number(currency.replace(/[^0-9\.-]+/g, ""));
    return number;
}

function notification(notifyType, message) {
    $.notify({
        title: '<strong>' + notifyType + '!</strong>',
        message: message
    }, {
        type: notifyType
    });
}

function createLoaderOnTopOfElement(elementName, spinnerAtTop = false) {
    var element = $("#" + elementName);
    element.css({ "position": "relative" });
    if (spinnerAtTop) {
        element.prepend("<div id='loader' class='loading-style'><div class='spinner-outer'><div class='spinner-inner loader loader-lg'></div></div></div>");
    } else {
        element.prepend("<div id='loader' class='loading-style'><div class='spinner-outer'><div class='spinner-inner loader loader-lg'></div></div></div>");
    }
}

function removeLoaderFromElement(elementName) {
    var element = $("#" + elementName);
    element.find("#loader").remove();
}



