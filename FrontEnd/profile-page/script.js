const displayUsername = async () => {
  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const username = getUsernameFromJwt();
  const heading = document.querySelector("h1");
  heading.innerHTML += ` ${username}!`;
};

const isAdmin = () => {
  const decodedToken = getDecodedJwtToken();

  const roleClaim =
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

  return decodedToken[roleClaim] === "Admin";
};

const removeExistingErrorContainer = () => {
  const div = document.querySelector(".error-container");
  if (div) {
    div.remove();
  }
};

const displayEmptyFieldError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "emptyField");

  div.innerHTML = `
    <h1 class="error-title">The field is empty!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const addDeleteEndpoint = () => {
  const div = document.createElement("div");
  div.id = "delete-endpoint";
  div.innerHTML = `
    <h2>Delete user</h2>
    <input
    type="text"
    name="delete-id"
    id="delete-id"
    placeholder="Id"
    autocomplete="off"
    required
    />
    <br />
    <br />
    <button onclick="onDelete()">Delete</button>
  `;

  document.querySelector("#endpoints-container").append(div);
};

const onDelete = async () => {
  const id = document.querySelector("#delete-id").value;

  if (!id) {
    displayEmptyFieldError();
    console.error("ID is required");
    return;
  }

  if (!confirm(`Are you sure you want to delete user '${id}'?`)) {
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  try {
    const url = `https://localhost:7066/api/User/${id}`;
    const response = await fetch(url, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      if (response.status === 404) {
        displayNotFoundError();
        throw new Error(`Invalid id!`);
      }
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("User deleted successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const displayNotFoundError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "userNotFound");

  div.innerHTML = `
    <h1 class="error-title">Invalid Id!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const onShowPersonInfo = async () => {
  const id = getUserIdFromJwt();

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  try {
    const url = `https://localhost:7066/api/User/${id}`;
    const response = await fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    const json = await response.json();

    displayJsonData(json);
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const displayJsonData = (jsonData) => {
  const div = document.querySelector("#display-personInfo");
  div.innerHTML = `
    <p>Name: ${jsonData.firstName} ${jsonData.lastName}</p>
    <p>Personal id number: ${jsonData.personalIdNumber}</p>
    <p>Phone number: ${jsonData.phoneNumber}</p>
    <p>Email: ${jsonData.email}</p>
    <p>Photo:</p>
    <img></img>
    <p>Residence: ${jsonData.residence.city}, ${jsonData.residence.street} ${jsonData.residence.houseNumber}-${jsonData.residence.roomNumber}</p>
  `;

  const base64Data = `data:image;base64,${jsonData.photoBytes}`;
  const imgElement = document.querySelector("img");
  imgElement.src = base64Data;

  div.classList.add("visible");
};

const onUpdateUsername = async () => {
  const newUsername = document.querySelector("#update-username").value;

  if (!newUsername) {
    displayEmptyFieldError();
    console.error("Username is required");
    return;
  }

  usernameValidation(newUsername);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newUsername,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateUsername`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Username updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
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

const onUpdatePassword = async () => {
  const currentPassword = document.querySelector("#update-password-old").value;
  const newPassword = document.querySelector("#update-password-new").value;

  if (!currentPassword || !newPassword) {
    displayEmptyFieldError();
    console.error("Current and new username is required");
    return;
  }

  passwordValidation(newPassword);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    currentPassword,
    newPassword,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdatePassword`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      if (response.status === 401) {
        displayUnauthorizedError();
        throw new Error(`Invalid password!`);
      }
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Password updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
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
    <h1 class="error-title">New password must be at least 8 characters long, include uppercase, lowercase, a number, and a special character!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const displayUnauthorizedError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "userNotFound");

  div.innerHTML = `
    <h1 class="error-title">Old password is wrong!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const onUpdateName = async () => {
  const firstName = document.querySelector("#update-firstName").value;
  const lastName = document.querySelector("#update-lastName").value;

  if (!firstName && !lastName) {
    displayEmptyFieldError();
    console.error("Both fields are empty. Provide at least one name.");
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    NewFirstName: firstName || null,
    NewLastName: lastName || null,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateName`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    console.log(JSON.stringify(data));
    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Name update request processed successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdatePersonalIdNumber = async () => {
  const newPersonalIdNumber = document.querySelector(
    "#update-personalIdNumber"
  ).value;

  if (!newPersonalIdNumber) {
    displayEmptyFieldError();
    console.error("Personal id number is required");
    return;
  }

  personalIdNumberValidation(newPersonalIdNumber);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newPersonalIdNumber,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdatePersonalIdNumber`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Personal id number updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
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

const onUpdatePhoneNumber = async () => {
  const newPhoneNumber = document.querySelector("#update-phoneNumber").value;

  if (!newPhoneNumber) {
    displayEmptyFieldError();
    console.error("Phone number is required");
    return;
  }

  phoneNumberValidation(newPhoneNumber);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newPhoneNumber,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdatePhoneNumber`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Phone number updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

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

const onUpdateEmail = async () => {
  const newEmail = document.querySelector("#update-email").value;

  if (!newEmail) {
    displayEmptyFieldError();
    console.error("Email is required");
    return;
  }

  emailValidation(newEmail);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newEmail,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateEmail`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Email updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
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

const onUpdatePhoto = async () => {
  const newPhotoInput = document.querySelector("#update-photo");
  const newPhoto = newPhotoInput.files[0];

  if (!newPhoto) {
    displayEmptyFieldError();
    console.error("Photo is not selected");
    return;
  }

  photoValidation(newPhoto);

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const formData = new FormData();
  formData.append("newPhoto", newPhoto);

  try {
    const url = `https://localhost:7066/api/Profile/UpdatePhoto`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
      },
      body: formData,
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Photo updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
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

const onUpdateCity = async () => {
  const newCity = document.querySelector("#update-city").value;

  if (!newCity) {
    displayEmptyFieldError();
    console.error("City is required");
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newCity,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateCity`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("City updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateStreet = async () => {
  const newStreet = document.querySelector("#update-street").value;

  if (!newStreet) {
    displayEmptyFieldError();
    console.error("Street is required");
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newStreet,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateStreet`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Street updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateHouseNumber = async () => {
  const newHouseNumber = document.querySelector("#update-houseNumber").value;

  if (!newHouseNumber) {
    displayEmptyFieldError();
    console.error("House number is required");
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newHouseNumber,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateHouseNumber`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("House number updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateRoomNumber = async () => {
  const newRoomNumber = document.querySelector("#update-roomNumber").value;

  if (!newRoomNumber) {
    displayEmptyFieldError();
    console.error("Room number is required");
    return;
  }

  const token = localStorage.getItem("JWT");
  if (!token) {
    console.error("No token found. User is not authenticated.");
    return;
  }

  const data = {
    newRoomNumber,
  };

  try {
    const url = `https://localhost:7066/api/Profile/UpdateRoomNumber`;
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Room number updated successfully.");
    location.reload();
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const getUsernameFromJwt = () => {
  const decodedToken = getDecodedJwtToken();

  const roleClaim =
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

  return decodedToken[roleClaim];
};

const getUserIdFromJwt = () => {
  const decodedToken = getDecodedJwtToken();

  const roleClaim =
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

  return decodedToken[roleClaim];
};

const getDecodedJwtToken = () => {
  const token = localStorage.getItem("JWT");

  if (!token) {
    return false;
  }

  return parseJwt(token);
};

const parseJwt = (token) => {
  if (!token) {
    return;
  }

  const base64Url = token.split(".")[1];
  const base64 = base64Url.replace("-", "+").replace("_", "/");

  return JSON.parse(window.atob(base64));
};

const initialData = () => {
  displayUsername();
  if (isAdmin()) {
    addDeleteEndpoint();
  }
};

window.onload = initialData;
