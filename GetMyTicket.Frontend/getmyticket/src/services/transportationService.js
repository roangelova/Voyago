import { api } from "../api/api.js";

export const Transportation = {
    getAll: () => api.get('api/transportations'),
}