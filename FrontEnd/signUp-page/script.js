const onSignUp = async () => {
  const username = document.querySelector("#signin-name").value.trim();
  const password = document.querySelector("#signin-password").value;

  if (!username || !password) {
    // TODO: display error
    return;
  }

  // TODO: add validations

  const data = { username, password };

  try {
    const url = "";
  } catch {}
};
