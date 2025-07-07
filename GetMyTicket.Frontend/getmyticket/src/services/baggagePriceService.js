import { api } from "../api/api.js";

export const BaggagePrices = {
    getPricesForProvider: (tpId) => api.get(`api/BaggagePrices/${tpId}`),
}