const onSignUp = async () => {
  const username = document.querySelector("#signup-username").value.trim();
  const password = document.querySelector("#signup-password").value;
  const firstName = document.querySelector("#signup-firstName").value;
  const lastName = document.querySelector("#signup-lastName").value;
  const personalId = document.querySelector("#signup-personalId").value;
  const phoneNumber = document.querySelector("#signup-phoneNumber").value;
  const email = document.querySelector("#signup-email").value;
  const photoPath = document.querySelector("#signup-photo").value;
  const city = document.querySelector("#signup-city").value;
  const street = document.querySelector("#signup-street").value;
  const houseNumber = document.querySelector("#signup-houseNumber").value;
  const roomNumber = document.querySelector("#signup-roomNumber").value;

  if (
    !username ||
    !password ||
    !firstName ||
    !lastName ||
    !personalId ||
    !phoneNumber ||
    !email ||
    !photoPath ||
    !city ||
    !street ||
    !houseNumber ||
    !roomNumber
  ) {
    // TODO: display error
    console.error("All fields are required.");
    return;
  }

  // TODO: add validations

  const data = {
    username,
    password,
    personInfo: {
      firstName,
      lastName,
      personalId,
      phoneNumber,
      email,
      photoPath,
      residence: {
        city,
        street,
        houseNumber,
        roomNumber,
      },
    },
  };

  console.log("Payload sent to server:", JSON.stringify(data, null, 2));

  try {
    const url = "https://localhost:7066/api/User/SignUp";
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

    logIn(username, password);
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const logIn = async (username, password) => {
  if (!username || !password) {
    // TODO: display error
    return;
  }

  // TODO: add validations

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

    window.location.href = "../main-page/main.html"; // go to main page
  } catch (error) {
    console.error("Detailed error:", error);
  }
};
