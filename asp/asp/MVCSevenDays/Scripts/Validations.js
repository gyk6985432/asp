function IsFirstNameEmpty(){
    if (document.getElementById("TxtFName").value=="") {
        return 'First Name should not be empty!'
    }
    else {
        return '';
    }
}

function IsFirstNameInValid() {
    if (document.getElementById('TxtFName').value.indexOf("@")!=-1) {
        return 'First Name should not contains @!'
    }
    else {
        return '';
    }
}

function IsLastNameInValid() {
    if (document.getElementById('TxtLName').value.length>5) {
        return 'Last Name should not contain more than 5 characters !'
    }
    else {
        return '';
    }
}

function IsSaliaryEmpty() {
    if (document.getElementById('TxtSalary').value=="") {
        return 'Salary should not be empty!'
    }
    else {
        return '';
    }
}

function IsSalaryInValid() {
    if (isNaN(document.getElementById('TxtSalary').value)) {
        return 'Enter valid salary!'
    }
    else {
        return '';
    }
}

function IsValid() {
    var FirstNameEmptyMessage = IsFirstNameEmpty();
    var FirstNameInValidMessage = IsFirstNameInValid();
    var LastNameInValidMessage = IsLastNameInValid();
    var SalaryEmptyMessage = IsSaliaryEmpty();
    var SalaryInValidMessage = IsSalaryInValid();
    var FinalMessage = "Errors: ";

    if (FirstNameEmptyMessage!="") {
        FinalMessage += "\n" + FirstNameEmptyMessage;
    }
    if (FirstNameInValidMessage!="") {
        FinalMessage += "\n" + FirstNameInValidMessage;
    }
    if (LastNameInValidMessage!="") {
        FinalMessage += "\n" + LastNameInValidMessage;
    }
    if (SalaryEmptyMessage!="") {
        FinalMessage += "\n" + SalaryEmptyMessage;
    }
    if (SalaryInValidMessage!="") {
        FinalMessage += "\n" + SalaryInValidMessage;
    }

    if (FinalMessage!="Errors: ") {
        alert(FinalMessage);
        return false;
    }
    else {
        return true;
    }
}