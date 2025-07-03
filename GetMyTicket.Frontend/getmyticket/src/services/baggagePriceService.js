import { api } from "../api/api.js";

export const BaggagePrice = {
    getPricesForProvider: (tpId) => api.get(`api/BaggagePrice/${tpId}`),
}