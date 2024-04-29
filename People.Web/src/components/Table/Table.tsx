import React from "react";
import "./Table.css";
import Button from "../Button/Button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";

interface TableColumn<T> {
  title: string;
  className?: (item: T) => React.ReactNode;
  render: (item: T) => React.ReactNode;
}

interface TableProps<T> {
  data: T[];
  columns: TableColumn<T>[];
  customButtons?: ((item: T) => React.ReactNode)[];
  onUpdate?: (item: T) => void;
  onDelete?: (item: T) => void;
}

const Table = <T extends { id: string }>({
  data,
  columns,
  customButtons,
  onUpdate,
  onDelete,
}: TableProps<T>) => {
  return (
    <table className="table">
      <thead>
        <tr>
          {columns.map((column, index) => (
            <th key={index}>{column.title}</th>
          ))}
          {(customButtons || onUpdate || onDelete) && (
            <th className="table-action-column">Actions</th>
          )}
        </tr>
      </thead>
      <tbody>
        {data.map((item) => (
          <tr key={item.id}>
            {columns.map((column, index) => (
              <td
                key={index}
                className={`table-cell ${
                  column.className && column.className(item)
                }`}
              >
                <div className="cell-main-content">{column.render(item)}</div>
              </td>
            ))}
            {(customButtons || onUpdate || onDelete) && (
              <td className="table-action-column">
                {customButtons &&
                  customButtons.map((customButton, index) => (
                    <div key={index} className="custom-button">
                      {customButton(item)}
                    </div>
                  ))}
                {onUpdate && (
                  <Button
                    className="table-button edit"
                    onClick={() => onUpdate(item)}
                  >
                    <FontAwesomeIcon icon={faEdit} />
                  </Button>
                )}
                {onDelete && (
                  <Button
                    className="table-button delete"
                    onClick={() => onDelete(item)}
                  >
                    <FontAwesomeIcon icon={faTrash} />
                  </Button>
                )}
              </td>
            )}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default Table;
