

var months = [];
months['January'] = 1
months['February'] = 2
months['March'] = 3
months['April'] = 4
months['May'] = 5
months['June'] = 6
months['July'] = 7
months['August'] = 8
months['September'] = 9
months['October'] = 10
months['November'] = 11
months['December'] = 12


var monthsArray = [];
monthsArray[1] = 'January';
monthsArray[2] = 'February';
monthsArray[3] = 'March';
monthsArray[4] = 'April';
monthsArray[5] = 'May';
monthsArray[6] = 'June';
monthsArray[7] = 'July';
monthsArray[8] = 'August';
monthsArray[9] = 'September';
monthsArray[10] = 'October';
monthsArray[11] = 'November';
monthsArray[12] = 'December';



var shallowCopy = function (orginal) {
    // First create an empty object with
    // same prototype of our original source
    var clone = Object.create(Object.getPrototypeOf(orginal));

    var i, keys = Object.getOwnPropertyNames(orginal);
    for (i = 0; i < keys.length; i += 1) {
        // copy each property into the clone
        Object.defineProperty(clone, keys[i],
            Object.getOwnPropertyDescriptor(orginal, keys[i])
        );
    }
    return clone;
};


var populateSelectInput = function (selectInput, dataItemArray, selectedValue) {
    selectInput.empty();
    $.each(dataItemArray, function (key, val) {
        var selected = "";

        if (val.Value == selectedValue) {
            selected = "selected";
        }
        selectInput.append("<option value=" + val.Key + " " + selected + " >" + val.Value + "</option");
    });
}



var ExistsInArrayByField = function (array, field, value) {
    var returnValue = false;

    $.each(array, function (key, val) {
        $.each(val, function (subKey, subVal) {
            if (subKey == field) {
                found = true;
            }
        });
    });

    return returnValue;
}



var getSelectInputSelectedText = function (selectInput) {
    var returnValue = null;

    $.each(selectInput, function (key, val) {
        if (val.selected) {
            returnValue = val.innerText;
        }
    })

    return returnValue;
}

var setTimeoutMessage = function (tag, message) {
    setInputValue(tag, message);
    $(this).delay(1000).queue(function () {
        $(this).dequeue();
        setInputValue(tag, "");
    });
};


var removeItemFromArray = function (array, keyName, item) {
    var inc = 0;

    $.each(array, function (key, val) {
        if (val[keyName] === item) {
            return false;
        }
        inc++;
    });

    array.splice(inc, 1);
};



var setInputValue = function (input, value) {
    var result = null;

    switch (input.tagName) {
        case "SPAN":
            {
                input.innerText = value;
                break;
            }
        case "H4":
            {
                input.innerText = value;
                break;
            }
        case "INPUT":
            {
                switch (input.type) {

                    case "text":
                        {
                            input.value = value;
                            break;
                        }
                    case "checkbox":
                        {
                            input.checked = value;
                            break;
                        }
                    default:
                        {
                            input.value = value;
                            break;
                        }
                }
                break;
            }
        case "checkbox":
            {
                result = input.checked;
                break;
            }
        case "select-one":
        case "text":
            {
                result = input.value;
                break;
            }
        default:
            {
                result = input.value;
                break;
            }
    }

    return result;
}



var getInputValue = function (input) {
    var result = null;

    switch (input.type) {
        case "checkbox":
            {
                result = input.checked;
                break;
            }
        case "select-one":
        case "text":
            {
                result = input.value;
                break;
            }
        default:
            {
                result = input.value;
                break;
            }
    }

    return result;
}

var getCurrentDate = function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;

    return today;
}


var formatDateTime = function (input) {
    var today = new Date(input);

    var today2 = new Date();

    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    var hh = today2.getHours();
    var mins = today2.getMinutes();
    var ss = right('00' + today2.getSeconds(), 2);

    var ampm = "AM";

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    if (hh >= 12) {
        ampm = "PM";
    }

    today = mm + '/' + dd + '/' + yyyy + ' ' + hh + ':' + mins + ':' + ss + ' ' + ampm;

    return today;

}


var getCurrentDateAndTime = function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    var hh = today.getHours();
    var mins = today.getMinutes();
    var ss = right('00' + today.getSeconds(), 2);

    var ampm = "AM";

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    if (hh >= 12) {
        ampm = "PM";
    }

    today = mm + '/' + dd + '/' + yyyy + ' ' + hh + ':' + mins + ':' + ss + ' ' + ampm;

    return today;
}




function right(str, chr) {
    return str.slice(-(chr));
}
function left(str, chr) {
    return str.slice(0, chr);
}