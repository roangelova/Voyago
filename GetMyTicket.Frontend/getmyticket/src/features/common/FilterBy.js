import { useSearchParams } from "react-router-dom";

function FilterBy({ title = null, options = [], param = null }) {
    const [params, setParams] = useSearchParams();

    const handleSelect = (e) => {
        params.set(param, e.target.value);
        setParams(params)
    }

    if (!title || !options || options?.length <= 0 || !param) return;

    return (
        <div className="filterBy">
            <label htmlFor="filterBy">Filter: </label>
            <select id="filterBy" onChange={handleSelect}>
                <optgroup label={title}>

                    {options.map(opt =>
                        <option key={opt.key} value={opt.value}>{opt.key}</option>
                    )}

                </optgroup>
            </select>
        </div>
    )
}

export default FilterBy;


