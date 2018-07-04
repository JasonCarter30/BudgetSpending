

var InputControl = function (divContainer, objectType, inputName, inputTitle, defaultValue, dataList)
{
    this.objectType = objectType;
    this.divContainer = divContainer;
    this.inputTitle = inputTitle;
    this.defaultValue = defaultValue;
    this.inputName = inputName;
    this.dataList = dataList;
    
    this.inputEntered = function (value) {
        return value;
    }

    this.BuildInputControl = function () {
        var html = "";

        var controlId = "txtControl" + this.inputName;

        var btnSaveId = "btnSave" + this.inputName;

        switch (this.objectType) {
            case "string": {

                if (this.dataList != undefined && this.dataList.DataItems.length > 0)
                {
                    html += "<div class='form-group'>";
                    html += "<label>" + this.inputTitle + "</label>";
                    html += "        <select class='form-control' id='" + controlId + "'></select>";
                    html += "</div>";

                    this.divContainer.append(html);

                    var options = $("#" + controlId);

                    populateSelectInput(options, this.dataList.DataItems, this.dataList.DefaultValue);

                    
                  

                    var inputControl = this;

                    $(options).on("change", function (e) {
                        //var inputValue = getInputValue(options[0]);

                        var inputValue = getSelectInputSelectedText(options[0]);
                        inputControl.inputEntered(inputValue);
                    })
                }
                else {
                    html += this.inputTitle;
                    html += "<div class='input-group'>";
                    html += "   <input type='text' id='" + controlId + "' class='form-control'>";
                    html += "   <span class='input-group-btn'>";
                    html += "       <button class='btn btn-default' id='" + btnSaveId + "' type='button'>Save</button>";
                    html += "   </span>";
                    html += "</div>";

                    this.divContainer.append(html);

                    var ctlTextBox = this.divContainer.find("#" + controlId)

                    setInputValue(ctlTextBox[0], defaultValue);
                    var btnSave = this.divContainer.find("#" + btnSaveId);
                    var inputControl = this;

                    $(btnSave).on("click", function (e) {
                        var inputValue = getInputValue(ctlTextBox[0]);
                        inputControl.inputEntered(inputValue);
                    })
                }

                
                
                

                break;
            }
            case "boolean":
            {
                html += "<div class='checkbox'>";
                html += "   <label><input type='checkbox' id='" + controlId + "' value=" + this.defaultValue  + ">" + this.inputTitle + "</label>";
                html += "</div>";

                this.divContainer.append(html);
                var ctlTextBox = this.divContainer.find("#" + controlId)

                setInputValue(ctlTextBox[0], defaultValue);

                var btnSave = this.divContainer.find("#btnSave")
                var inputControl = this;

                $(ctlTextBox).on("click", function (e) {
                    var inputValue = getInputValue(ctlTextBox[0]);
                    inputControl.inputEntered(inputValue);
                })
                break;
            }
            default:
                {
                    break;
                }


        }

        return html;
    }

    
}





