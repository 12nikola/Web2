import { useState, useEffect } from "react";
import { Container, Card, Button, Form, Spinner, Modal, Table } from "react-bootstrap";
import axiosInstance from "../../../../axios";

const CategoryManager = () => {
  const [categoryList, setCategoryList] = useState([]);
  const [pending, setPending] = useState(true);
  const [newName, setNewName] = useState("");
  const [editing, setEditing] = useState(null); 
  const [processing, setProcessing] = useState(false);

  const loadCategories = async () => {
    setPending(true);
    try {
      const res = await axiosInstance.get("/categories");
      setCategoryList(res.data || []);
    } catch (err) {
      console.error("Error loading categories", err);
    } finally {
      setPending(false);
    }
  };

  useEffect(() => {
    loadCategories();
  }, []);

  const removeCategory = async (id) => {
    try {
      await axiosInstance.delete(`/categories/${id}`);
      setCategoryList(categoryList.filter((c) => c.categoryID !== id));
    } catch (err) {
      console.error("Error deleting category", err);
    }
  };

  const saveUpdate = async () => {
    if (!editing.name.trim()) return;

    setProcessing(true);
    try {
      const res = await axiosInstance.patch(`/categories/${editing.id}`, {
        name: editing.name,
      });
      setCategoryList(
        categoryList.map((c) =>
          c.categoryID === res.data.categoryID ? res.data : c
        )
      );
      setEditing(null);
    } catch (err) {
      console.error("Error updating category", err);
    } finally {
      setProcessing(false);
    }
  };

  const addCategory = async () => {
    if (!newName.trim()) return;

    setProcessing(true);
    try {
      const res = await axiosInstance.post("/categories", { name: newName });
      setCategoryList([...categoryList, res.data]);
      setNewName("");
    } catch (err) {
      console.error("Error creating category", err);
    } finally {
      setProcessing(false);
    }
  };

  if (pending) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  return (
    <Container className="my-4">
      <Card className="mb-4 p-3">
        <h3 className="d-flex justify-content-center">Manage Categories</h3>
        <Form className="d-flex mt-3 mb-3 justify-content-center">
          <Form.Control
            type="text"
            placeholder="New Category Name"
            value={newName}
            onChange={(e) => setNewName(e.target.value)}
            isInvalid={newName.trim().length > 0 && newName.trim().length < 3}
          />
          <Button
            variant="success"
            className="ms-2"
            onClick={addCategory}
            disabled={processing || newName.trim().length < 3}
          >
            Add
          </Button>
        </Form>

        <Table striped bordered hover responsive>
          <thead>
            <tr>
              <th className="text-center" style={{ width: "50%" }}>Name</th>
              <th className="text-center" style={{ width: "50%" }}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {categoryList.map((cat) => (
              <tr key={cat.categoryID}>
                <td className="text-center">{cat.name}</td>
                <td className="text-center">
                  {cat.modifiable && (
                    <>
                      <Button
                        variant="primary"
                        size="sm"
                        className="me-2"
                        onClick={() =>
                          setEditing({ id: cat.categoryID, name: cat.name })
                        }
                      >
                        Update
                      </Button>
                      <Button
                        variant="danger"
                        size="sm"
                        onClick={() => removeCategory(cat.categoryID)}
                      >
                        Delete
                      </Button>
                    </>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Card>

      <Modal show={!!editing} onHide={() => setEditing(null)}>
        <Modal.Header closeButton>
          <Modal.Title>Update Category</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control
            type="text"
            value={editing?.name || ""}
            onChange={(e) => setEditing({ ...editing, name: e.target.value })}
            isInvalid={
              editing?.name.trim().length > 0 && editing?.name.trim().length < 3
            }
          />
          <Form.Control.Feedback type="invalid">
            Category name must be at least 3 characters long.
          </Form.Control.Feedback>
        </Modal.Body>

        <Modal.Footer className="justify-content-center">
          <Button variant="secondary" onClick={() => setEditing(null)}>
            Cancel
          </Button>
          <Button
            variant="primary"
            onClick={saveUpdate}
            disabled={processing || editing?.name.trim().length < 3}
          >
            Save
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};

export default CategoryManager;
