import { useState } from "react";

import arrrowToLeft from '../../assets/icons/arrowToLeft.png'
import arrrowToRight from '../../assets/icons/arrowToRight.png'

function Pagination({ totalItemsCount = 1 }) {
    const resultsPerPageOptions = [5, 10, 15, 20];
    const [resultsPerPage, setResultsPerPage] = useState(resultsPerPageOptions[0]);
    const [currentPage, setCurrentPage] = useState(1);

    let totalNumberOfPages = Math.ceil(totalItemsCount / resultsPerPage);

    const handlePreviousPage = () => {
        if (currentPage === 1) return;
        setCurrentPage(currentPage => currentPage === 1 ? 1 : currentPage - 1)
    }

    const handleNextPage = () => {
        if (currentPage === totalNumberOfPages) return;
        setCurrentPage(currentPage => currentPage === totalNumberOfPages ? totalNumberOfPages : currentPage + 1)
    }

    return (
        <div className="pagination">
            <div className="pagination__pages">
                <span>
                    <img onClick={handlePreviousPage} src={arrrowToLeft} />
                </span>
                <div>
                    Page {currentPage} of {totalNumberOfPages}
                </div>
                <span onClick={handleNextPage}>
                    <img src={arrrowToRight} />
                </span>
            </div>
            <div className="pagination__resultsPerPage">
                <label htmlFor="resultPerPage">Results per page: </label>
                <select id="resultPerPage" value={resultsPerPage} onChange={(e) => setResultsPerPage(e.target.value)}>

                    {resultsPerPageOptions.map(opt =>
                        <option value={opt}>{opt}</option>
                    )}

                </select>
            </div>
        </div>
    )
}

export default Pagination;