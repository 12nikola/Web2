class QuizInfo {
  constructor({ id, heading, details, level, editable, duration, active, createdOn, questionIds }) {
    this.id = id;
    this.heading = heading;
    this.details = details;
    this.level = level;
    this.editable = editable;
    this.duration = QuizInfo.toMilliseconds(duration);
    this.active = active;
    this.createdOn = new Date(createdOn);
    this.questionIds = questionIds || [];
  }

  static toMilliseconds(timeStr) {
    if (!timeStr) return null;

    const parts = timeStr.split(':').map(Number);
    if (parts.length === 3) {
      const [h, m, s] = parts;
      return (h * 3600 + m * 60 + s) * 1000;
    }
    return null;
  }

  static createList(dataArray) {
    return dataArray.map((d) => new QuizInfo(d));
  }

  static createSingle(data) {
    return new QuizInfo(data);
  }
}

export default QuizInfo;
