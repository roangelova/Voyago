import { api } from "../api/api.js";

export const Account = {
    register: (data) => api.post('api/Account/register', data),
    login: (data) => api.post('api/Authentication/login', data)
}