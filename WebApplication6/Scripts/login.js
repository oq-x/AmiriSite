const query = window.location.search;


const registerLink = document.getElementById("registerLink");
registerLink.href = "Register" + query;
/**
 * Login function
 * @param {MouseEvent} e
 */
function login(e) {
    const emailInput = document.getElementById("email") if (emailInput.value == "" || !emailInput.checkValidity()) {
        setError("Invalid email!")
        e.preventDefault()
        return
    } else {
        setError("")
    }
    const passwordInput = document.getElementById("password")
    if (passwordInput.value == "") {
        setError("No password provided!")
        e.preventDefault()
    } else {
        setError("")
    }
}

/**
 * Set the error text
 * @param {String} text
 */
function setError(text) {
    const errorText = document.getElementById("error")
    errorText.innerHTML = text
}