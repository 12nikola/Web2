class Option {
  constructor({ id, label, correct }) {
    this.id = id;
    this.label = label;
    this.correct = correct || false;
  }

  static createList(dataArray) {
    return dataArray?.map((d) => new Option(d)) || [];
  }

  static createSingle(data) {
    return new Option(data);
  }
}

export default Option;
