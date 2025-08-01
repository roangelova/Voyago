import Cookies from "js-cookie";

export function formatDate(date) {
  return new Intl.DateTimeFormat("en-GB", {
    weekday: "long",
    day: "numeric",
    month: "long",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hourCycle: "h24",
  }).format(new Date(date));
  //Monday 19 May 2025 at 13:40
}

export function getFormattedDate(date) {
  return new Intl.DateTimeFormat("en-GB", {
    weekday: "long",
    day: "numeric",
    month: "long",
    year: "numeric",
  }).format(new Date(date));
}

export function getFormattedDob(date) {
  return new Intl.DateTimeFormat("en-GB", {
    day: "numeric",
    month: "numeric",
    year: "numeric",
  }).format(new Date(date));
}

export function getFormattedBookingDate(date) {
  return new Intl.DateTimeFormat("en-GB", {
    day: "numeric",
    month: "long",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hourCycle: "h24",
  }).format(new Date(date));
}

export function getFormattedTime(date) {
  return new Intl.DateTimeFormat("en-GB", {
    hour: "2-digit",
    minute: "2-digit",
    hourCycle: "h24",
  }).format(new Date(date));
}

//This function sets the access and refresh tokens in the cookies
export function setAccessAndRefreshTokenInCookies(response) {
  Cookies.set(
    "accessToken",
    JSON.stringify({
      accessToken: response.accessToken,
      tokenType: response.tokenType,
      expires: response.accessTokenExpires,
    }),
    {
      expires: new Date(response.accessTokenExpires),
      secure: true,
      sameSite: true,
    }
  );

  Cookies.set(
    "refreshToken",
    JSON.stringify({ refreshToken: response.refreshToken }),
    {
      expires: new Date(response.refreshTokenExpires),
      secure: true,
      sameSite: true,
    }
  );

  window.dispatchEvent(new Event("refreshUser"));
}

//Check if the user/passenger with the provided date string is at least 18 years old
export function isAtLeast18(dobString) {
  const dob = new Date(dobString);
  const today = new Date();

  const age = today.getFullYear() - dob.getFullYear();
  const hasHadBirthdayThisYear =
    today.getMonth() > dob.getMonth() ||
    (today.getMonth() === dob.getMonth() && today.getDate() >= dob.getDate());

  return age > 18 || (age === 18 && hasHadBirthdayThisYear);
}

export function calculateTotalPrice(trip, passengers) {

   return passengers.adults * trip.adultPrice +
    passengers.children * trip.childrenPrice
   }