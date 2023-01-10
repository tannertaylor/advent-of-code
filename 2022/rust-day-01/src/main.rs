use std::fs;

fn main() {
    println!("Part 1: {}", sum_max_cals(1));
    println!("Part 2: {}", sum_max_cals(3));
}

fn sum_max_cals(count: usize) -> i32 {
    let mut cal_sums: Vec<i32> = fs::read_to_string("input.txt").unwrap()
        .split("\n\n")
        .filter(|section| !section.is_empty())
        .map(|section| section
             .split('\n')
             .map(|cals| cals.parse::<i32>().unwrap())
             .sum::<i32>()
        ).collect();
    cal_sums.sort_by(|a, b| b.cmp(a));
    cal_sums.iter().take(count).sum()
}
