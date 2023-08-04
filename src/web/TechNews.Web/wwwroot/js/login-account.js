const configureElements = () => {
    let elements = document.querySelectorAll('.input100');

    elements.forEach(element => {
        element.addEventListener('blur', function () {
            if (this.value.trim() !== "") {
                this.classList.add('has-val');
            } else {
                this.classList.remove('has-val');
            }

            if (validate(this) === false) {
                showValidate(this);
            }
            else {
                hideValidate(this);
            }

            if (this.name === 'pass') {
                let confirmPass = document.getElementById('txtConfirmPassword');

                if (validate(confirmPass) === false) {
                    showValidate(confirmPass);
                }
                else {
                    hideValidate(confirmPass);
                }
            }

            if (this.name === 'confirm-pass') {
                let pass = document.getElementById('txtPassword');

                if (validate(pass) === false) {
                    showValidate(pass);
                }
                else {
                    hideValidate(pass);
                }
            }
        });
    });

    var inputs = document.querySelectorAll('.validate-input .input100');

    document.querySelector('.validate-form').addEventListener('submit', function (event) {
        var check = true;

        for (var i = 0; i < inputs.length; i++) {
            if (validate(inputs[i]) === false) {
                showValidate(inputs[i]);
                check = false;
            }
        }

        if (!check) {
            event.preventDefault();
        }
    });

    inputs.forEach(function (element) {
        element.addEventListener('focus', function () {
            hideValidate(this);
        });
    });

    var showPass = 0;

    document.querySelectorAll('.btn-show-pass').forEach(function (button) {
        button.addEventListener('click', function () {
            var input = this.nextElementSibling;

            if (showPass === 0) {
                input.setAttribute('type', 'text');
                this.querySelector('i').classList.remove('zmdi-eye');
                this.querySelector('i').classList.add('zmdi-eye-off');
                showPass = 1;
            } else {
                input.setAttribute('type', 'password');
                this.querySelector('i').classList.add('zmdi-eye');
                this.querySelector('i').classList.remove('zmdi-eye-off');
                showPass = 0;
            }
        });
    });
};

const validate = (input) => {
    if (input.type === 'email' || input.name === 'email') {
        if (!input.value.trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/)) {
            return false;
        }
    } else if (input.name === 'confirm-pass') {
        let passwordInput = document.getElementById('txtPassword');
        if (!(input.value.trim() === passwordInput.value.trim()) || input.value.trim() === '') {
            return false;
        }
    }
    else {
        if (input.value.trim() === '') {
            return false;
        }
    }

    return true;
};

const showValidate = (input) => {
    var thisAlert = input.parentElement;
    thisAlert.classList.add('alert-validate');
};

const hideValidate = (input) => {
    var thisAlert = input.parentElement;
    thisAlert.classList.remove('alert-validate');
};


configureElements();