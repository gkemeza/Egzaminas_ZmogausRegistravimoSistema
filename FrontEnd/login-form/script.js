const onLogin = async () => {
  const username = document.querySelector("#login-name").value.trim();
  const password = document.querySelector("#login-password").value;

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
