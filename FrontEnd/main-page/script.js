const isAdmin = () => {
  const token = localStorage.getItem("JWT");

  if (!token) {
    return false;
  }

  const decodedToken = parseJwt(token);
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
