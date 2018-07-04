var BaseModal = function ()
{

var modalHTML = "";
    modalHTML += "<div id='BaseModal' class='modal fade' role='dialog'>";
    modalHTML += "  <div class='modal-dialog'>";
    modalHTML += "    <div class='modal-content'>";
    modalHTML += "      <div id='BaseModalHeader' class='modal-header'>";
    modalHTML += "          <button type='button' class='close' data-dismiss='modal'>&times;</button>";
    modalHTML += "          <h4 class='modal-title' id='BaseModalTitle'></h4>";
    modalHTML += "      </div>";
    modalHTML += "      <div id='BaseModalBody' class='modal-body'>";
    modalHTML += "      </div>";
    modalHTML += "      <div class='modal-footer'>";
    modalHTML += "          <button type='button' class='btn btn-default' data-dismiss='modal'>Close</button>";
    modalHTML += "      </div>";
    modalHTML += "    </div>";
    modalHTML += "  </div>";
    modalHTML += "</div>";
 

    if ($('#BaseModal').length > 0) {
        $('#BaseModal').remove();
    }

    $('body').append(modalHTML);

    this.Modal = $('#BaseModal');

    this.BaseModalTitle = this.Modal.find("#" + "BaseModalTitle");


    this.BaseModalBodyContainer = this.Modal.find("#" + "BaseModalBody");

    Object.defineProperty(BaseModal.prototype, "Title", {
        configurable: true,
        get: function () {
            var result = getInputValue(this.BaseModalTitle[0])
            return result;
        },
        set: function (value) {
            setInputValue(this.BaseModalTitle[0], value)
        }
    });

    
}

