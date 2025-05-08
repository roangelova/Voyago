import { useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";

//TODO -> out of sync issue
function FilterBy({ title = null, options = [], paramName = null }) {
    const [params, setParams] = useSearchParams();
    const [filterValue, setFilterValue] = useState(options[0]?.value);

    useEffect(() => {
        params.set(paramName, filterValue);
        setParams(params);

    }, [filterValue, params, setParams])

    if (!title || !options || options?.length <= 0 || !paramName) return;

    return (
        <div className="filterBy">
            <label htmlFor="filterBy">{title}: </label>
            <select id="FilterBy" value={filterValue} onChange={(e) => setFilterValue(e.target.value)}>

                {options?.map(opt =>
                    <option key={opt.key} value={opt.value}>{opt.key}</option>
                )}

            </select>
        </div>
    )
}

export default FilterBy;


