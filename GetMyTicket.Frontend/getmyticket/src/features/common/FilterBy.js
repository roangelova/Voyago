import { useSearchParams } from "react-router-dom";

function FilterBy({data}) {
    const [params, setParams] = useSearchParams();

    const handleSelect = (e) => {
        params.set("provider", e.target.value);
        setParams(params)
    }

    //TODO - MAKE IT DYNAMIC & REUSIBLE! DATA SHOULD COME FROM THE OUTSIDE!

    return (
        <div className="filterBy">
            <label htmlFor="filterBy">Filter: </label>
            <select id="filterBy" onChange={handleSelect}>
            <optgroup label="Transportation Provider"> 
                <option defaultChecked value={'all'}>Show all</option>
                <option value={'TransAvia'}>Transavia</option>
                <option value={'Avianca'}>Avianca</option>
                <option value={'DeutscheBahn'}>Deutsche Bahn</option>
           </optgroup>
            </select>
        </div>
    )
}

export default FilterBy;


