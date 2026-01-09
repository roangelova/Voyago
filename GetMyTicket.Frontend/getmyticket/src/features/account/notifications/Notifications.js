import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

function createData(title, content, received) {
  return { title, content, received };
}

const rows = [
  createData(
    "Your trip XYZ122 with RyanAir has been canceled.",
    "Hi, Christopher. We are sorry to inform your booking with reference XYZ122 has been...",
    "09.11.2025"
  ),
  createData(
    "Welcome to VOYAGO! Claim your 10% off now!",
    "Hey, there! Welcome to VOYAGO! We are happy that you are here. As a gift to ...",
    "07.11.2025"
  ),
];

export default function Notifications() {
  return (
    <TableContainer component={Paper}>
      <Table className="muiTable" aria-label="Notifications table">
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell>Content</TableCell>
            <TableCell>Received</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <TableRow key={row.name}>
              <TableCell>{row.title}</TableCell>
              <TableCell>{row.content}</TableCell>
              <TableCell>{row.received}</TableCell>
              <TableCell className="muiTable__actions">...</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
