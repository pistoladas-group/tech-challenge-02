const btnLoginElement = document.getElementById('btnLogin');
const btnAccountElement = document.getElementById('btnAccount');
const inputElements = document.querySelectorAll('.validate-input .input100');
const emailElement = document.getElementById('txtEmail');
const passwordElement = document.getElementById('txtPassword');
const confirmPasswordElement = document.getElementById('txtConfirmPassword');
const divWarningAlertElement = document.getElementById('divWarningAlert');
const divErrorAlertElement = document.getElementById('divErrorAlert');

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

            if (this.name === 'pass' && confirmPasswordElement !== null) {
                if (validate(confirmPasswordElement) === false) {
                    showValidate(confirmPasswordElement);
                }
                else {
                    hideValidate(confirmPasswordElement);
                }
            }

            if (this.name === 'confirm-pass') {
                if (validate(passwordElement) === false) {
                    showValidate(passwordElement);
                }
                else {
                    hideValidate(passwordElement);
                }
            }
        });
    });

    if (btnLoginElement != null) {
        btnLoginElement.addEventListener('click', () => {
            let check = true;

            check = validateForm();

            if (check) {
                login();
            }
        });
    }

    if (btnAccountElement != null) {
        btnAccountElement.addEventListener('click', () => {
            let check = true;

            check = validateForm();

            if (check) {
                createAccount();
            }
        });
    }

    inputElements.forEach(function (element) {
        element.addEventListener('focus', function () {
            hideValidate(this);
        });
    });

    let showPass = false;

    document.querySelectorAll('.btn-show-pass').forEach(function (button) {
        button.addEventListener('click', function () {
            let input = this.nextElementSibling;

            if (showPass === false) {
                input.setAttribute('type', 'text');
                this.querySelector('i').classList.remove('zmdi-eye');
                this.querySelector('i').classList.add('zmdi-eye-off');
                showPass = true;
            } else {
                input.setAttribute('type', 'password');
                this.querySelector('i').classList.add('zmdi-eye');
                this.querySelector('i').classList.remove('zmdi-eye-off');
                showPass = false;
            }
        });
    });
};

const validateForm = () => {
    let check = true;

    for (let i = 0; i < inputElements.length; i++) {
        if (validate(inputElements[i]) === false) {
            showValidate(inputElements[i]);
            check = false;
        }
    }

    return check;
};

const validate = (input) => {
    if (input === null) {
        return;
    }
    if (input.type === 'email' || input.name === 'email') {
        if (!input.value.trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/)) {
            return false;
        }
    } else if (input.name === 'confirm-pass') {
        if (!(input.value.trim() === passwordElement.value.trim()) || input.value.trim() === '') {
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
    let thisAlert = input.parentElement;
    thisAlert.classList.add('alert-validate');
};

const hideValidate = (input) => {
    let thisAlert = input.parentElement;
    thisAlert.classList.remove('alert-validate');
};

const hideAlerts = () => {
    divWarningAlertElement.classList.add('d-none');
    divErrorAlertElement.classList.add('d-none');
};

const showWarningAlert = () => {
    divWarningAlertElement.classList.remove('d-none');
};

const showErrorAlert = (message) => {
    let defaultMessage = "Ocorreu um erro inesperado, por favor tente novamente ou contate o suporte";

    if (message !== null && message !== undefined) {
        defaultMessage = message;
    }

    divErrorAlertElement.textContent = defaultMessage;
    divErrorAlertElement.classList.remove('d-none');
};

const login = () => {
    PageLoading.show();
    hideAlerts();

    let data = {
        email: emailElement.value,
        password: passwordElement.value
    };

    fetch('login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            PageLoading.hide();

            if (response.status === 400) {
                showErrorAlert("Usuário ou senha inválidos");
                return;
            }

            if (response.status === 403) {
                showWarningAlert();
                return;
            }

            if (response.status === 500) {
                showErrorAlert();
                return;
            }

            // TODO: Passar username para mostrar um Olá!
            window.location.href = '/home';
        })
        .catch(error => {
            PageLoading.hide();
            showErrorAlert();
        });
};

const createAccount = () => {
    let data = {
        id: generateGuid(),
        email: emailElement.value,
        password: passwordElement.value,
        repassword: confirmPasswordElement.value
    };

    fetch('', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (response.ok) {
                window.location.href = '/home';
            } else {
                // Handle login failure
            }
        })
        .then(data => {
            let response = JSON.parse(data);
            // TODO: Definir o que vai fazer...
            // Acho que se o login funcionar, não tem que fazer nada
            // porq vai redirecionar para a Home
        })
        .catch(error => {
            // TODO: Definir o que vai fazer... mostrar um toaster talvez?
            console.error('Error:', error);
        });
};

const generateGuid = () => {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
};

configureElements();