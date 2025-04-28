import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

function SortBy({sortOptions = []}) {
    const [params, setParams] = useSearchParams();
    const [sortValue, setSortValue] = useState(sortOptions[0]?.value);

    useEffect(() => {
        params.set("sortBy", sortValue);
        setParams(params)
    }, [sortValue, params, setParams])

    if (!sortOptions || sortOptions.length <= 0) return;

    return (
        <div className="sortBy">
            <label htmlFor="sortBy">Sort: </label>
            <select id="SortBy" value={sortValue} onChange={(e) => setSortValue(e.target.value)}>

                {sortOptions?.map(opt => <option key={opt.key} value={opt.value}>{opt.key}</option>)}

            </select>
        </div>
    )
}

export default SortBy;