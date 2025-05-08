import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

//TODO -> out of sync issue
function SortBy({sortOptions = []}) {
    const [params, setParams] = useSearchParams();
    const [sortValue, setSortValue] = useState(sortOptions[0]?.value);

    useEffect(() => {
        let newParams = new URLSearchParams(params);
        newParams.set("sortBy", sortValue);
        setParams(newParams)
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