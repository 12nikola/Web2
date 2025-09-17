class QuizSummary {
  constructor({ id, heading, createdOn, level, editable, active, details }) {
    this.id = id;
    this.heading = heading;
    this.createdOn = new Date(createdOn);
    this.level = level;
    this.editable = editable;
    this.active = active;
    this.details = details;
  }

  static createList(dataArray) {
    return dataArray.map((item) => new QuizSummary(item));
  }
}

export default QuizSummary;
