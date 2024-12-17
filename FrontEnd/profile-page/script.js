const isAdmin = () => {
  const decodedToken = getDecodedJwtToken();

  const roleClaim =
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  console.log("Decoded token role: ", decodedToken[roleClaim]);

  return decodedToken[roleClaim] === "Admin";
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
    // TODO: display error
    console.error("ID is required");
    return;
  }

  // TODO: add validations

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
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("User deleted successfully.");
  } catch (error) {
    console.error("Detailed error:", error);
  }
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
    console.log("User person data: ", json);

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
    // TODO: display error
    console.error("Username is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdatePassword = async () => {
  const currentPassword = document.querySelector("#update-password-old").value;
  const newPassword = document.querySelector("#update-password-new").value;

  if (!currentPassword || !newPassword) {
    // TODO: display error
    console.error("Current and new username is required");
    return;
  }

  // TODO: add validations

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
      throw new Error(`Error status: ${response.status}`);
    }

    console.log("Password updated successfully.");
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateName = async () => {
  const firstName = document.querySelector("#update-firstName").value;
  const lastName = document.querySelector("#update-lastName").value;

  if (!firstName && !lastName) {
    // TODO: display error
    console.error("Both fields are empty. Provide at least one name.");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdatePersonalIdNumber = async () => {
  const newPersonalIdNumber = document.querySelector(
    "#update-personalIdNumber"
  ).value;

  if (!newPersonalIdNumber) {
    // TODO: display error
    console.error("Personal id number is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdatePhoneNumber = async () => {
  const newPhoneNumber = document.querySelector("#update-phoneNumber").value;

  if (!newPhoneNumber) {
    // TODO: display error
    console.error("Phone number is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateEmail = async () => {
  const newEmail = document.querySelector("#update-email").value;

  if (!newEmail) {
    // TODO: display error
    console.error("Email is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdatePhoto = async () => {
  const newPhotoInput = document.querySelector("#update-photo");
  const newPhoto = newPhotoInput.files[0];

  if (!newPhoto) {
    // TODO: display error
    console.error("Photo is not selected");
    return;
  }

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateCity = async () => {
  const newCity = document.querySelector("#update-city").value;

  if (!newCity) {
    // TODO: display error
    console.error("City is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateStreet = async () => {
  const newStreet = document.querySelector("#update-street").value;

  if (!newStreet) {
    // TODO: display error
    console.error("Street is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateHouseNumber = async () => {
  const newHouseNumber = document.querySelector("#update-houseNumber").value;

  if (!newHouseNumber) {
    // TODO: display error
    console.error("House number is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const onUpdateRoomNumber = async () => {
  const newRoomNumber = document.querySelector("#update-roomNumber").value;

  if (!newRoomNumber) {
    // TODO: display error
    console.error("Room number is required");
    return;
  }

  // TODO: add validations

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};

const getUserIdFromJwt = () => {
  const decodedToken = getDecodedJwtToken();

  const roleClaim =
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
  console.log("Decoded token id: ", decodedToken[roleClaim]);

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
  if (isAdmin()) {
    addDeleteEndpoint();
  }
};

window.onload = initialData;