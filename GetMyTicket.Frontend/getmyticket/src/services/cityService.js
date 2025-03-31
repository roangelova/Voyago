import { useQuery } from "@tanstack/react-query";
import { api } from "../api/api.js";

export function useGetCities(){
    const {isPending, error, data: cities} = useQuery({
        queryKey: ['cities'],
        queryFn: () => api.get('api/cities')
    })

    return {isPending, cities, error};
}