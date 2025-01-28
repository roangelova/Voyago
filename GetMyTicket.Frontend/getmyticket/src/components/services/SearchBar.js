const SearchBar = () => {

    //CURRENT BASIC STRUCTURE: 
    //START
    //end
    //START DAY 
    //END DAY 


    return (
        <div className="searchBar__container">
            <div className='searchBar__container--element'>
                <select name="start">
                    <option value="munich">Munich</option>
                    <option value="varna">Varna</option>
                    <option value="Sofia">Sofia</option>
                </select>
            </div>

            <div className='searchBar__container--element'>
                <select name="destination">
                    <option value="munich">Munich</option>
                    <option value="varna">Varna</option>
                    <option value="Sofia">Sofia</option>
                </select>
            </div>

            <div className='searchBar__container--element'>
                <input
                    name="startDate"
                    type="date"
                    value='2025-05-10'
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <input
                    name="endDate"
                    type="date"
                    value='2025-05-18'
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <button className='searchBar__button'>
                    Search
                </button>
            </div>

        </div>
    )

}

export default SearchBar;