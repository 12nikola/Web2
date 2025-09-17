import Option from "../Models/Option";

class Question {
  constructor({ id, kind, label, category, choices, correct }) {
    this.id = id;
    this.kind = kind; // SingleChoice, MultipleChoice, TrueFalse, FillIn
    this.label = label;
    this.category = category;
    this.givenAnswer = "";
    this.givenAnswers = [];

    if (kind === "SingleOption" || kind === "MultipleOption") {
      this.choices = Option.createList(choices);
    } else {
      this.choices = [];
    }

    this.correct = correct ? new Option(correct) : null;
  }

  static createList(dataArray) {
    return dataArray?.map((item) => new Prompt(item)) || [];
  }

  toPayload() {
    const result = { QuestionID: this.id };

    if (this.kind === "Boolean" && this.givenAnswer) {
      result.TrueFalseUserAnswer = this.givenAnswer === "True";
    } else if ((this.kind === "SingleOption" || this.kind === "FillIn") && this.givenAnswer) {
      result.SingleUserAnswer = this.givenAnswer;
    } else if (this.kind === "MultipleOption" && this.givenAnswers?.length > 0) {
      result.MultipleUserAnswers = this.givenAnswers;
    }

    return result;
  }
}

export default Prompt;