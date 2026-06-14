// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const burgerButton = document.getElementById("burgerButton");
const mobileMenu = document.getElementById("mobileMenu");

if (burgerButton && mobileMenu) {
  burgerButton.addEventListener("click", () => {
    const isOpen = mobileMenu.classList.toggle("is-open");
    burgerButton.setAttribute("aria-expanded", isOpen);
  });
}

document.addEventListener("DOMContentLoaded", () => {
  const password = document.getElementById("Password");
  const error = document.getElementById("passwordError");

  if (!password || !error) return;

  password.addEventListener("input", () => {
    const value = password.value;

    const valid =
      value.length >= 8 &&
      /[A-Z]/.test(value) &&
      /[a-z]/.test(value) &&
      /\d/.test(value) &&
      /[^A-Za-z0-9]/.test(value);

    error.textContent = valid
      ? ""
      : "Password must contain uppercase, lowercase, number and special character.";
  });


});

document.addEventListener("DOMContentLoaded", () => {
  const nameRegex = /^[\p{L}\s'-]+$/u;
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const phoneRegex = /^[0-9+\-\s()]+$/;

  const validateInput = (inputId, errorId, validator, message, allowEmpty = false) => {
    const input = document.getElementById(inputId);
    const error = document.getElementById(errorId);

    if (!input || !error) return;

    input.addEventListener("input", () => {
      const value = input.value.trim();

      if (allowEmpty && value === "") {
        error.textContent = "";
        return;
      }

      error.textContent = validator(value) ? "" : message;
    });
  };

  validateInput(
    "FirstName",
    "firstNameError",
    value => value.length > 0 && nameRegex.test(value),
    "First name may only contain letters, spaces, apostrophes and hyphens."
  );

  validateInput(
    "LastName",
    "lastNameError",
    value => value.length > 0 && nameRegex.test(value),
    "Last name may only contain letters, spaces, apostrophes and hyphens."
  );

  validateInput(
    "Email",
    "profileEmailError",
    value => value.length > 0 && emailRegex.test(value),
    "Please enter a valid email address."
  );

  validateInput(
    "PhoneNumber",
    "phoneError",
    value => phoneRegex.test(value),
    "Phone number may only contain numbers, spaces, +, -, and parentheses.",
    true
  );
});