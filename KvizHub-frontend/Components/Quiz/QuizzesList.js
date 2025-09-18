import { useState, useEffect } from "react";
import { Card, Button, Container, Row, Col, Pagination, Spinner, Form } from "react-bootstrap";
import axiosInstance from "../../../axios";
import SimplifiedQuiz from "../../../KvizHub/KvizHub/Models/Answers";
import { useNavigate } from "react-router-dom";
import Category from "../../../KvizHub/KvizHub/Models/Solution";

function QuizzesList({ user }) {
  const [offset, setOffset] = useState(0);
  const [quizList, setQuizList] = useState([]);
  const [pending, setPending] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [categoryList, setCategoryList] = useState([]);

  const [searchKeyword, setSearchKeyword] = useState("");
  const [difficultyFilter, setDifficultyFilter] = useState("");
  const [categoryFilter, setCategoryFilter] = useState("");
  const [filterType, setFilterType] = useState(null);
  const [activeFilters, setActiveFilters] = useState({});

  const navigate = useNavigate();
  const limit = 5;

  const loadCategories = async () => {
    try {
      const res = await axiosInstance.get("categories");
      setCategoryList(Category.fromJsonArray(res.data));
    } catch (err) {
      console.error("Failed to fetch categories", err);
    }
  };

  const loadQuizzes = async () => {
    setPending(true);
    try {
      let url = `quizzes?offset=${offset}&limit=${limit}`;

      if (activeFilters.keyword) url += `&keyword=${encodeURIComponent(activeFilters.keyword)}`;
      if (activeFilters.difficulty) url += `&DifficultyLevel=${encodeURIComponent(activeFilters.difficulty)}`;
      if (activeFilters.categoryId) url += `&categoryId=${activeFilters.categoryId}`;

      const res = await axiosInstance.get(url);
      setQuizList(SimplifiedQuiz.fromJsonArray(res.data.quizzes));
      setTotalCount(res.data.numberOfQuizzes || 0);
    } catch (err) {
      console.error("Failed to fetch quizzes", err);
    } finally {
      setPending(false);
    }
  };

  useEffect(() => {
    loadCategories();
  }, []);

  useEffect(() => {
    loadQuizzes();
  }, [offset, activeFilters]);

  const applyFilters = () => {
    const updatedFilters = { ...activeFilters };

    if (filterType === "keyword") {
      searchKeyword.trim() ? (updatedFilters.keyword = searchKeyword.trim()) : delete updatedFilters.keyword;
    }

    if (filterType === "difficulty") {
      difficultyFilter ? (updatedFilters.difficulty = difficultyFilter) : delete updatedFilters.difficulty;
    }

    if (filterType === "category") {
      categoryFilter ? (updatedFilters.categoryId = categoryFilter) : delete updatedFilters.categoryId;
    }

    setOffset(0);
    setActiveFilters(updatedFilters);
  };

  const resetFilters = () => {
    setSearchKeyword("");
    setDifficultyFilter("");
    setCategoryFilter("");
    setFilterType(null);
    setActiveFilters({});
    setOffset(0);
  };

  const viewRanking = (quizId) => navigate(`${quizId}/ranking`);
  const editQuiz = (quizId) => navigate(`${quizId}/update`);
  const removeQuiz = async (quizId) => {
    try {
      await axiosInstance.delete(`quizzes/${quizId}`);
      loadQuizzes();
    } catch (err) {
      console.error("Failed to remove quiz", err);
    }
  };
  const solveQuiz = (quizId) => navigate(`${quizId}/solve`);

  const totalPages = Math.ceil(totalCount / limit);
  const currentPage = Math.floor(offset / limit) + 1;

  return (
    <Container className="my-4">
      <Card className="mb-4">
        <Card.Body>
          <div className="d-flex flex-wrap mb-3">
            <Button
              variant={filterType === "keyword" ? "dark" : "outline-dark"}
              className="me-2 mb-2"
              onClick={() => setFilterType("keyword")}
            >
              Filter by Keyword
            </Button>
            <Button
              variant={filterType === "difficulty" ? "dark" : "outline-dark"}
              className="me-2 mb-2"
              onClick={() => setFilterType("difficulty")}
            >
              Filter by Difficulty
            </Button>
            <Button
              variant={filterType === "category" ? "dark" : "outline-dark"}
              className="me-2 mb-2"
              onClick={() => setFilterType("category")}
            >
              Filter by Category
            </Button>
          </div>

          {filterType === "keyword" && (
            <Form.Control
              type="text"
              placeholder="Enter keyword"
              value={searchKeyword}
              onChange={(e) => setSearchKeyword(e.target.value)}
              className="mb-2"
            />
          )}

          {filterType === "difficulty" && (
            <Form.Select
              value={difficultyFilter}
              onChange={(e) => setDifficultyFilter(e.target.value)}
              className="mb-2"
            >
              <option value="">Select difficulty</option>
              <option value="Easy">Easy</option>
              <option value="Medium">Medium</option>
              <option value="Hard">Hard</option>
            </Form.Select>
          )}

          {filterType === "category" && (
            <Form.Select
              value={categoryFilter}
              onChange={(e) => setCategoryFilter(e.target.value)}
              className="mb-2"
            >
              <option value="">Select category</option>
              {categoryList.map((c) => (
                <option key={c.categoryID} value={c.categoryID}>
                  {c.name}
                </option>
              ))}
            </Form.Select>
          )}

          <div className="d-flex flex-wrap">
            {(
              (filterType === "keyword" && searchKeyword.trim()) ||
              (filterType === "difficulty" && difficultyFilter) ||
              (filterType === "category" && categoryFilter)
            ) && (
              <Button variant="primary" className="me-2 mb-2" onClick={applyFilters}>
                Apply Filters
              </Button>
            )}

            {Object.keys(activeFilters).length > 0 && (
              <Button variant="secondary" className="mb-2" onClick={resetFilters}>
                Reset Filters
              </Button>
            )}
          </div>
        </Card.Body>
      </Card>

      {pending ? (
        <div className="d-flex justify-content-center my-5">
          <Spinner animation="border" />
        </div>
      ) : quizList.length === 0 ? (
        <p className="text-center my-5">No quizzes available.</p>
      ) : (
        <>
          <Row className="g-3">
            {quizList.map((quiz) => (
              <Col key={quiz.quizID} xs={12}>
                <Card>
                  <Card.Body>
                    <Card.Title className="text-center mb-2">{quiz.title}</Card.Title>
                    <div className="d-flex justify-content-between mb-3">
                      <span>Difficulty: {quiz.difficultyLevel}</span>
                      <span className="text-muted">Created: {quiz.creationDate.toLocaleDateString()}</span>
                    </div>
                    <div className="d-flex justify-content-center flex-wrap">
                      <Button
                        variant="success"
                        size="sm"
                        disabled={!quiz.available}
                        className="me-2 mb-2"
                        onClick={() => solveQuiz(quiz.quizID)}
                      >
                        Solve Quiz
                      </Button>
                      <Button
                        variant="primary"
                        size="sm"
                        className="me-2 mb-2"
                        disabled={quiz.modifiable}
                        onClick={() => viewRanking(quiz.quizID)}
                      >
                        View Rankings
                      </Button>
                      {user?.role === "admin" && (
                        <>
                          <Button
                            variant="warning"
                            size="sm"
                            className="me-2 mb-2"
                            disabled={!quiz.modifiable}
                            onClick={() => editQuiz(quiz.quizID)}
                          >
                            Edit
                          </Button>
                          <Button
                            variant="danger"
                            size="sm"
                            className="mb-2"
                            disabled={!quiz.modifiable}
                            onClick={() => removeQuiz(quiz.quizID)}
                          >
                            Remove
                          </Button>
                        </>
                      )}
                    </div>
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
          {totalPages > 1 && (
            <Pagination className="mt-3 justify-content-center">
              {Array.from({ length: totalPages }, (_, i) => (
                <Pagination.Item
                  key={i + 1}
                  active={i + 1 === currentPage}
                  onClick={() => setOffset((i) * limit)}
                >
                  {i + 1}
                </Pagination.Item>
              ))}
            </Pagination>
          )}
        </>
      )}
    </Container>
  );
}

export default QuizzesList;
