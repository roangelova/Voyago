function SortBy({ sortOptions = [], sortValue, setSortValue }) {
  if (!sortOptions || sortOptions.length <= 0) return;

  return (
    <div className="sortBy">
      <label htmlFor="sortBy">Sort: </label>
      <select
        id="SortBy"
        value={sortValue}
        onChange={(e) => setSortValue(e.target.value)}
      >
        {sortOptions?.map((opt) => (
          <option key={opt.key} value={opt.value}>
            {opt.key}
          </option>
        ))}
      </select>
    </div>
  );
}

export default SortBy;
