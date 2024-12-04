const onSignUp = async () => {
  const username = document.querySelector("#signup-name").value.trim();
  const password = document.querySelector("#signup-password").value;

  if (!username || !password) {
    // TODO: display error
    return;
  }

  // TODO: add validations

  const data = { username, password };

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
  } catch (error) {
    console.error("Detailed error:", error);
  }
};
