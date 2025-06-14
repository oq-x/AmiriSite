/**
 * Login function
 * @param {MouseEvent} e
 */
function register(e) {
    const emailInput = document.getElementById("email")
    if (emailInput.value == "" || !emailInput.checkValidity()) {
        setError("Invalid email!")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const usernameInput = document.getElementById("username")
    if (usernameInput.value.length < 3 || usernameInput.value.length > 50) {
        setError("Username must be 3 to 50 characters long!")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const firstNameInput = document.getElementById("fname")
    if (firstNameInput.value.length < 3 || firstNameInput.value.length > 30) {
        setError("First name must be 3 to 30 characters long!")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const lastNameInput = document.getElementById("lname")
    if (lastNameInput.value.length < 3 || lastNameInput.value.length > 30) {
        setError("Last name must be 3 to 30 characters long!")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const passwordInput = document.getElementById("password")
    if (passwordInput.value.length < 8) {
        setError("Password must be at least 8 characters long!")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const phoneInput = document.getElementById("phone")
    if (phoneInput.value === "" || !phoneInput.checkValidity()) {
        setError("Phone number must be in this format: 0XX-XXXXXXX")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const birthdayInput = document.getElementById("birthday")
    if (birthdayInput.value === "" || !birthdayInput.checkValidity()) {
        setError("You must input your birthday")
        e.preventDefault()
        return
    } else {
        setError("")
    }

    const gender = document.querySelector('input[name="gender"]:checked');
    if (!gender) {
        setError("You must select a gender!")
        e.preventDefault()
        return
    } else {
        setError("")
        window.hiddenGender.value = gender.id;
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