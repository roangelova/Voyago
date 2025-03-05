import { api } from "../api/api.js";

export const Cities = {
    getAll: () => api.get('api/cities'),
}