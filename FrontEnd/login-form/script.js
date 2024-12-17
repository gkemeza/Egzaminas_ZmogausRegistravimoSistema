const onLogin = async () => {
  const username = document.querySelector("#login-name").value.trim();
  const password = document.querySelector("#login-password").value;

  if (!username || !password) {
    displayEmptyFieldsError();
    return;
  }

  try {
    passwordValidation(password);
  } catch (error) {
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
      if (response.status === 400) {
        displayBadRequestError();
        throw new Error(`Username or password is wrong!`);
      }
    }

    const content = await response.json();
    const token = content.token;
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
    <h1 class="error-title">Username or password is wrong!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};

const displayBadRequestError = () => {
  removeExistingErrorContainer();
  const div = document.createElement("div");
  div.classList.add("error-container", "userNotFound");

  div.innerHTML = `
    <h1 class="error-title">Username or password is wrong!</h1>
    <button class="close-error-button">Close</button>
  `;

  document.body.append(div);

  const closeButton = div.querySelector(".close-error-button");
  closeButton.addEventListener("click", () => {
    div.remove();
  });
};
