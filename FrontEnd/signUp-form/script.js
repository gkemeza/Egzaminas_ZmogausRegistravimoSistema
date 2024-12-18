const onSignUp = async () => {
  const username = document.querySelector("#signup-username").value.trim();
  const password = document.querySelector("#signup-password").value;
  const firstName = document.querySelector("#signup-firstName").value;
  const lastName = document.querySelector("#signup-lastName").value;
  const personalIdNumber = document.querySelector(
    "#signup-personalIdNumber"
  ).value;
  const phoneNumber = document.querySelector("#signup-phoneNumber").value;
  const email = document.querySelector("#signup-email").value;
  const photoInput = document.querySelector("#signup-photo");
  const photo = photoInput.files[0];
  const city = document.querySelector("#signup-city").value;
  const street = document.querySelector("#signup-street").value;
  const houseNumber = document.querySelector("#signup-houseNumber").value;
  const roomNumber = document.querySelector("#signup-roomNumber").value;

  // Validations
  if (
    !username ||
    !password ||
    !firstName ||
    !lastName ||
    !personalIdNumber ||
    !phoneNumber ||
    !email ||
    !photoInput ||
    !photo ||
    !city ||
    !street ||
    !houseNumber ||
    !roomNumber
  ) {
    displayEmptyFieldsError();
    return;
  }

  try {
    usernameValidation(username);
    passwordValidation(password);
    personalIdNumberValidation(personalIdNumber);
    phoneNumberValidation(phoneNumber);
    emailValidation(email);
    photoValidation(photo);
  } catch (error) {
    return;
  }

  const formData = new FormData();
  formData.append("username", username);
  formData.append("password", password);

  formData.append("personInfo.firstName", firstName);
  formData.append("personInfo.lastName", lastName);
  formData.append("personInfo.personalIdNumber", personalIdNumber);
  formData.append("personInfo.phoneNumber", phoneNumber);
  formData.append("personInfo.email", email);
  formData.append("personInfo.photo", photo);

  formData.append("personInfo.residence.city", city);
  formData.append("personInfo.residence.street", street);
  formData.append("personInfo.residence.houseNumber", houseNumber);
  formData.append("personInfo.residence.roomNumber", roomNumber);

  console.log("FormData prepared for submission:");
  for (const [key, value] of formData.entries()) {
    console.log(`${key}:`, value);
  }

  try {
    const url = "https://localhost:7066/api/User/SignUp";
    const response = await fetch(url, {
      method: "POST",
      body: formData,
    });

    if (!response.ok) {
      const errorDetails = await response.text();
      throw new Error(
        `HTTP error! status: ${response.status}, details: ${errorDetails}`
      );
    }

    logIn(username, password);
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const logIn = async (username, password) => {
  if (!username || !password) {
    return;
  }

  const data = { username, password };

  try {
    const url = "https://localhost:7066/api/User/Login";
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    const content = await response.json();
    const token = content.token;
    console.log("Login response token: ", token);

    localStorage.setItem("JWT", token);

    window.location.href = "../profile-page/profile.html";
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const removeExistingErrorContainer = () => {
  const div = document.querySelector(".error-container");
  if (div) {
    div.remove();
  }
};

const displayEmptyFieldsError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "emptyFields");

  div.innerHTML = `
    <h1 class="error-title">All fields are required!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const usernameValidation = (username) => {
  const minLength = 3;

  if (minLength > username.length) {
    displayWrongUsernameError();
    throw new Error("Invalid username!");
  }
};

const displayWrongUsernameError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongUsername");

  div.innerHTML = `
    <h1 class="error-title">Username must be at least 3 characters long!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const passwordValidation = (password) => {
  const minLength = 8;
  const hasUpperCase = /[A-Z]/;
  const hasLowerCase = /[a-z]/;
  const hasDigit = /\d/;
  const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/;

  if (
    !(
      password.length >= minLength &&
      hasUpperCase.test(password) &&
      hasLowerCase.test(password) &&
      hasDigit.test(password) &&
      hasSpecialChar.test(password)
    )
  ) {
    displayWrongPasswordError();
    throw new Error("Invalid password!");
  }
};

const displayWrongPasswordError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongPassword");

  div.innerHTML = `
    <h1 class="error-title">Password must be at least 8 characters long, include uppercase, lowercase, a number, and a special character!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const personalIdNumberValidation = (personalIdNumber) => {
  const validationResult = validatePersonalId(personalIdNumber);
  if (validationResult) {
    displayWrongPersonalIdNumberError(validationResult);
    throw new Error("Invalid Personal Id Number");
  }
};

const displayWrongPersonalIdNumberError = (validationMessage) => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongPersonalIdNumber");

  div.innerHTML = `
    <h1 class="error-title">${validationMessage}</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const validatePersonalId = (personalIdNumber) => {
  if (isNaN(personalIdNumber)) {
    return "Personal ID number must be numeric!";
  }

  if (personalIdNumber.length !== 11) {
    return "Personal ID number must be exactly 11 digits!";
  }

  if (!isValidLithuanianPersonalIdChecksum(personalIdNumber)) {
    return "Invalid Personal ID number!";
  }

  return null;
};

function isValidLithuanianPersonalIdChecksum(personalIdNumber) {
  const weights1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2];
  const weights2 = [3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4];

  let sum = 0;

  // First iteration weights
  for (let i = 0; i < 10; i++) {
    sum += parseInt(personalIdNumber[i]) * weights1[i];
  }

  let checksum = sum % 11;

  if (checksum === 10) {
    // Second iteration weights
    sum = 0;
    for (let i = 0; i < 10; i++) {
      sum += parseInt(personalIdNumber[i]) * weights2[i];
    }

    checksum = sum % 11;

    if (checksum === 10) {
      checksum = 0;
    }
  }

  return checksum === parseInt(personalIdNumber[10]);
}

const phoneNumberValidation = (phoneNumber) => {
  const phoneNumberRegex = /^\+?[1-9]\d{1,14}$/;
  if (!phoneNumberRegex.test(phoneNumber)) {
    displayWrongPhoneNumberError();
    throw new Error("Invalid phone number");
  }
};

const displayWrongPhoneNumberError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongPhoneNumber");

  div.innerHTML = `
    <h1 class="error-title">Invalid Phone Number!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const emailValidation = (email) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailRegex.test(email)) {
    displayWrongEmailError();
    throw new Error("Invalid email");
  }
};

const displayWrongEmailError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongEmail");

  div.innerHTML = `
    <h1 class="error-title">Invalid email!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const photoValidation = (photo) => {
  const allowedExtensions = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];
  if (!isValidPhotoExtension(photo, allowedExtensions)) {
    displayWrongPhotoExtensionError(allowedExtensions);
    throw new Error(
      `Photo extension not allowed. Allowed extensions: ${allowedExtensions.join(
        ", "
      )}`
    );
  }
};

const isValidPhotoExtension = (photo, allowedExtensions) => {
  if (!photo) {
    throw new Error("No photo selected.");
  }

  const extension = photo.name.split(".").pop().toLowerCase();
  const fullExtension = `.${extension}`;

  if (!allowedExtensions.includes(fullExtension)) {
    return false;
  }

  return true;
};

const displayWrongPhotoExtensionError = (allowedExtensions) => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "wrongEmail");

  div.innerHTML = `
    <h1 class="error-title">Photo extension not allowed. Allowed extensions: ${allowedExtensions.join(
      ", "
    )}</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};
