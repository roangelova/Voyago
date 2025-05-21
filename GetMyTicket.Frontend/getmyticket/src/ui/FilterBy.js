function FilterBy({
  title = null,
  options = [],
  paramName = null,
  filterValue,
  setFilterValue,
}) {
  if (!title || !options || options?.length <= 0 || !paramName) return;

  return (
    <div className="filterBy">
      <label htmlFor="filterBy">{title}: </label>
      <select
        id="FilterBy"
        value={filterValue}
        onChange={(e) => setFilterValue(e.target.value)}
      >
        {options?.map((opt) => (
          <option key={opt.key} value={opt.value}>
            {opt.key}
          </option>
        ))}
      </select>
    </div>
  );
}

export default FilterBy;
