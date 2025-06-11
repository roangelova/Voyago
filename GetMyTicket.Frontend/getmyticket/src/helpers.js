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
}
