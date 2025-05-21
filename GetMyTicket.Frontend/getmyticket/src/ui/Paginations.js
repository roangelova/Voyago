import arrrowToLeft from "../assets/icons/arrowToLeft.png";
import arrrowToRight from "../assets/icons/arrowToRight.png";

function Pagination({
  totalItemsCount = 1,
  currentPage,
  setCurrentPage,
  resultsPerPage,
  setResultsPerPage,
  resultsPerPageOptions,
}) {
  let totalNumberOfPages = Math.ceil(totalItemsCount / resultsPerPage);

  const handlePreviousPage = () => {
    if (currentPage === 1) return;
    setCurrentPage((currentPage) => (currentPage === 1 ? 1 : currentPage - 1));
  };

  const handleNextPage = () => {
    if (currentPage === totalNumberOfPages) return;
    setCurrentPage((currentPage) =>
      currentPage === totalNumberOfPages ? totalNumberOfPages : currentPage + 1
    );
  };

  const handleNewResultsPerPageValue = (e) => {
    setResultsPerPage(e.target.value);
    setCurrentPage(1);
  };

  return (
    <div className="pagination">
      <div className="pagination__pages">
        <span>
          <img
            onClick={handlePreviousPage}
            src={arrrowToLeft}
            alt="Arrow to the left"
          />
        </span>
        <div>
          Page {currentPage} of{" "}
          {totalNumberOfPages === 0 ? 1 : totalNumberOfPages}
        </div>
        <span>
          <img
            onClick={handleNextPage}
            src={arrrowToRight}
            alt="Arrow to the right"
          />
        </span>
      </div>
      <div className="pagination__resultsPerPage">
        <label htmlFor="resultPerPage">Results per page: </label>
        <select
          id="resultPerPage"
          value={resultsPerPage}
          onChange={handleNewResultsPerPageValue}
        >
          {resultsPerPageOptions.map((opt) => (
            <option key={opt} value={opt}>
              {opt}
            </option>
          ))}
        </select>
      </div>
    </div>
  );
}

export default Pagination;
