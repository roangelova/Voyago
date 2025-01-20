import { api } from "../api/api.js";

export const Account = {
    register: (data) => api.post('Account/register', data)
}